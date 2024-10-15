using FoodApp.Api.VerticalSlicing.Common.MassTransit;
using FoodApp.Api.VerticalSlicing.Data.Entities;

namespace FoodApp.Api.VerticalSlicing.Features.Orders.UpdateOrderStatusTrip
{
    public class OrderStatusTripChangedSystemMessage : IOrderStatusTripChangedSystemMessage
    {
        public int OrderId { get; set; }

        public OrderStatusTrip NewStatusTrip { get; set; }

        public DateTime ChangeTime { get; set; }

        public string UserEmail { get; set; }
    }
}
