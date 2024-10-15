using FoodApp.Api.VerticalSlicing.Features.Orders.UpdateOrderStatusTrip;
using MassTransit;

namespace FoodApp.Api.VerticalSlicing.Common.MassTransit
{
    public class OrderTripStatusCustomerNotifierConsumer : IConsumer<IOrderStatusTripChangedMessage>
    {
     
            private readonly EmailSenderHelper _emailSenderHelper;

        public OrderTripStatusCustomerNotifierConsumer(EmailSenderHelper emailSenderHelper)
        {
            _emailSenderHelper = emailSenderHelper;
        }

        public async Task Consume(ConsumeContext<IOrderStatusTripChangedMessage> context)
        {
            var message = context.Message;

            string subject = "Order Status Trip Update";
            string body = $"Your order's trip with ID {message.OrderId} has been updated to {message.NewStatusTrip}.";

            await _emailSenderHelper.SendEmailAsync(message.UserEmail, subject, body);
        }

    }
}
