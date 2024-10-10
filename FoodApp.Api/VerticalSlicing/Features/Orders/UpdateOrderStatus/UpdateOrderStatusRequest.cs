using FoodApp.Api.VerticalSlicing.Data.Entities;

namespace FoodApp.Api.VerticalSlicing.Features.Orders.UpdateOrderStatus
{
    public record UpdateOrderStatusRequest
        (int OrderId, 
        OrderStatus NewStatus);
}
