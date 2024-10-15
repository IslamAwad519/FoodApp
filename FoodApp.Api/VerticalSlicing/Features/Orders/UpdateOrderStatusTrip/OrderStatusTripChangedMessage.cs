using FoodApp.Api.VerticalSlicing.Data.Entities;

namespace FoodApp.Api.VerticalSlicing.Features.Orders.UpdateOrderStatusTrip
{
    public class OrderStatusTripChangedMessage : IOrderStatusTripChangedMessage
    {
        public int OrderId { get; set; }

        public OrderStatusTrip NewStatusTrip { get; set; }

        public DateTime ChangeTime { get; set; }

        public string UserEmail { get; set; }
    }
}
