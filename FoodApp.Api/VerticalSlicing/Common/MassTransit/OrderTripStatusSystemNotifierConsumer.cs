using MassTransit;

namespace FoodApp.Api.VerticalSlicing.Common.MassTransit
{
    public class OrderTripStatusSystemNotifierConsumer : IConsumer<IOrderStatusTripChangedSystemMessage>
    {

        private readonly EmailSenderHelper _emailSenderHelper;

        public OrderTripStatusSystemNotifierConsumer(EmailSenderHelper emailSenderHelper)
        {
            _emailSenderHelper = emailSenderHelper;
        }

        public async Task Consume(ConsumeContext<IOrderStatusTripChangedSystemMessage> context)
        {
            var message = context.Message;

            string subject = "Order Status Trip Update";
            string body = $"Order's trip with ID {message.OrderId} has been updated to {message.NewStatusTrip} on time :{message.ChangeTime}";

            await _emailSenderHelper.SendEmailAsync(message.UserEmail, subject, body);
        }

    }
}
