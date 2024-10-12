
using FoodApp.Api.VerticalSlicing.Features.Invoices.GenerateInvoice;
using FoodApp.Api.VerticalSlicing.Features.Orders.CreateOrder;
using FoodApp.Api.VerticalSlicing.Features.Orders.UpdateOrderStatus;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace FoodApp.Api.VerticalSlicing.Common.RabbitMQServices
{
    public class RabbitMQConsumerService : IHostedService
    {

        IConnection _connection;
        IModel _channel;
        private readonly EmailSenderHelper _emailSenderHelper;

        public RabbitMQConsumerService(EmailSenderHelper emailSenderHelper)
        {
            _emailSenderHelper = emailSenderHelper;
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += Consumer_Received;
            _channel.BasicConsume(queue: "OrderStatus_Update_queue", autoAck: false, consumer: consumer);
            _channel.BasicConsume(queue: "InvoiceGenerated_queue", autoAck: false, consumer: consumer);
            _channel.BasicConsume(queue: "OrderCreated_queue", autoAck: false, consumer: consumer);

            return Task.CompletedTask;
        }

        private async void Consumer_Received(object? sender, BasicDeliverEventArgs e)
        {
            var messageBody = Encoding.UTF8.GetString(e.Body.ToArray());

            var routingKey = e.RoutingKey;
            if (routingKey == "key2")
            {
                var invoiceMessage = JsonConvert.DeserializeObject<InvoiceGeneratedMessage>(messageBody);
                if (invoiceMessage != null)
                {
                    string subject = "Invoice Generated";
                    string body = $"Your invoice for order ID {invoiceMessage.OrderId} has been generated. " +
                                  $"Total amount: {invoiceMessage.TotalAmount}. " +
                                  $"Generated at: {invoiceMessage.GeneratedAt}.";

                    await _emailSenderHelper.SendEmailAsync(invoiceMessage.UserEmail, subject, body);
                    _channel.BasicAck(e.DeliveryTag, false);
                    return;
                }
            }
            else if (routingKey == "key1")
            {
                var orderStatusUpdate = JsonConvert.DeserializeObject<OrderStatusUpdateMessage>(messageBody);
                if (orderStatusUpdate != null)
                {
                    string subject = "Order Status Update";
                    string body = $"Your order with ID {orderStatusUpdate.OrderId} has been updated to {orderStatusUpdate.NewStatus}.";
                    await _emailSenderHelper.SendEmailAsync(orderStatusUpdate.UserEmail, subject, body);
                    _channel.BasicAck(e.DeliveryTag, false);
                }
            }
            else if (routingKey == "key3")
            {
                var orderCreatedMessage = JsonConvert.DeserializeObject<OrderCreatedMessage>(messageBody);

                if (orderCreatedMessage != null)
                {
                    string subject = "Order Created";

                    string baseUrl = "https://localhost:7146/api/Order";
                    string acceptUrl = $"{baseUrl}/accept/{orderCreatedMessage.OrderId}";
                    string rejectUrl = $"{baseUrl}/reject/{orderCreatedMessage.OrderId}";

                    string body = $"The order with ID {orderCreatedMessage.OrderId} has been created.<br/>" +
                                  $"Total amount: {orderCreatedMessage.TotalPrice:C}.<br/>" +
                                  $"Created at: {orderCreatedMessage.CreatedAt}.<br/><br/>" +
                                  $"Order details:<br/>";

                    foreach (var item in orderCreatedMessage.OrderItems)
                    {
                        body += $"Recipe ID: {item.RecipeId}, Recipe Name: {item.RecipeName}, Quantity: {item.Quantity}<br/>";
                    }

                    body += $"<br/>Please review the order and take action:<br/>" +
                            $"<a href=\"{acceptUrl}\">Accept Order</a><br/>" +
                            $"<a href=\"{rejectUrl}\">Reject Order</a>";

                    await _emailSenderHelper.SendEmailAsync(orderCreatedMessage.UserEmail, subject, body);
                }

                _channel.BasicAck(e.DeliveryTag, false);
            }
            else
            {
                _channel.BasicReject(e.DeliveryTag, requeue: false);
            }
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _channel.Close();
            _connection.Close();
            return Task.CompletedTask;
        }
    }
}
