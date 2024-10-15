using FoodApp.Api.VerticalSlicing.Data.Entities;

namespace FoodApp.Api.VerticalSlicing.Features.Orders.UpdateOrderStatusTrip
{
    public class UpdateOrderStatusTripRequest
    {
        public int OrderId { get; set; }
        public OrderStatusTrip orderStatusTrip { get; set; }
    }
}
