using MassTransit;

namespace FoodApp.Api.VerticalSlicing.Common.MassTransit
{
    public class OrderTripStatusCustomerNotifierConsumer : IConsumer<IOrderStatusTripChangedCustomerMessage>
    {
     
            private readonly EmailSenderHelper _emailSenderHelper;

        public OrderTripStatusCustomerNotifierConsumer(EmailSenderHelper emailSenderHelper)
        {
            _emailSenderHelper = emailSenderHelper;
        }

        public async Task Consume(ConsumeContext<IOrderStatusTripChangedCustomerMessage> context)
        {
            var message = context.Message;

            string subject = "Order Status Trip Update";
            string body = $"Your order's trip with ID {message.OrderId} has been updated to {message.NewStatusTrip}.";

            await _emailSenderHelper.SendEmailAsync(message.UserEmail, subject, body);
        }

    }
}
