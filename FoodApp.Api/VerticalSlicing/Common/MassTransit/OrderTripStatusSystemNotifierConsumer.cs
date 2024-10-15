using FoodApp.Api.VerticalSlicing.Features.Orders.UpdateOrderStatusTrip;
using MassTransit;

namespace FoodApp.Api.VerticalSlicing.Common.MassTransit
{
    public class OrderTripStatusSystemNotifierConsumer : IConsumer<IOrderStatusTripChangedMessage>
    {

        private readonly EmailSenderHelper _emailSenderHelper;

        public OrderTripStatusSystemNotifierConsumer(EmailSenderHelper emailSenderHelper)
        {
            _emailSenderHelper = emailSenderHelper;
        }

        public async Task Consume(ConsumeContext<IOrderStatusTripChangedMessage> context)
        {
            var message = context.Message;

            string subject = "Order Status Trip Update";
            string body = $"Order's trip with ID {message.OrderId} has been updated to {message.NewStatusTrip}.";

            await _emailSenderHelper.SendEmailAsync("projectsmaster22@gmail.com", subject, body);
        }

    }
}
