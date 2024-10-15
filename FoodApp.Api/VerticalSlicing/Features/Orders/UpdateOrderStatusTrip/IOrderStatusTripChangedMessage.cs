using FoodApp.Api.VerticalSlicing.Data.Entities;

namespace FoodApp.Api.VerticalSlicing.Features.Orders.UpdateOrderStatusTrip
{
    public interface IOrderStatusTripChangedMessage
    {
        int OrderId { get; }
        OrderStatusTrip NewStatusTrip { get; }
        DateTime ChangeTime { get; }
        string UserEmail { get; }
    }
}
