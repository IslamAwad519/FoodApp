using FoodApp.Api.VerticalSlicing.Data.Entities;

namespace FoodApp.Api.VerticalSlicing.Common.MassTransit
{
    public interface IOrderStatusTripChangedSystemMessage
    {
        int OrderId { get; }
        OrderStatusTrip NewStatusTrip { get; }
        DateTime ChangeTime { get; }
        string UserEmail { get; }
    }
}
