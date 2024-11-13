using FoodApp.Api.VerticalSlicing.Data.Entities;

namespace FoodApp.Api.VerticalSlicing.Common.MassTransit
{
    public interface IOrderStatusTripChangedCustomerMessage
    {
        int OrderId { get; }
        OrderStatusTrip NewStatusTrip { get; }
        string UserEmail { get; }
    }
}
