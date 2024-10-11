using FoodApp.Api.VerticalSlicing.Features.Invoices.GenerateInvoice;
using FoodApp.Api.VerticalSlicing.Features.Orders.UpdateOrderStatus;
using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace FoodApp.Api.VerticalSlicing.Common.RabbitMQServices
{
    public class RabbitMQPublisherService
    {
        private readonly IConnection _connection;
        private readonly RabbitMQ.Client.IModel _channel;

        public RabbitMQPublisherService()
        {
            var factory = new ConnectionFactory() { HostName = "localhost"};
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare("OrderExchange", ExchangeType.Direct, true, false);
            _channel.ExchangeDeclare("NotificationExchange", ExchangeType.Direct, true, false);
            _channel.QueueDeclare("OrderStatus_Update_queue", durable: true, exclusive: false, autoDelete: false);
            _channel.QueueDeclare("InvoiceGenerated_queue", durable: true, exclusive: false, autoDelete: false);

            _channel.QueueBind("OrderStatus_Update_queue", "OrderExchange","key1");
            _channel.QueueBind("InvoiceGenerated_queue", "NotificationExchange", "key2");
        }

        public Result PublishMessage(OrderStatusUpdateMessage message)
        {
            var jsonMessage = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(jsonMessage);

            _channel.BasicPublish(exchange:"OrderExchange", routingKey: "key1", body: body);

            return Result.Success();
        }
        public Result PublishInvoiceMessage(InvoiceGeneratedMessage message)
        {
            var jsonMessage = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(jsonMessage);

            _channel.BasicPublish(exchange: "NotificationExchange", routingKey: "key2", body: body);

            return Result.Success();
        }
    }
}
