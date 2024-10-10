using FoodApp.Api.VerticalSlicing.Features.Common;

namespace FoodApp.Api.VerticalSlicing.Features.Orders
{
    public class OrderErrors
    {
        public static readonly Error OrderNotFound =
        new("Order not found.", StatusCodes.Status404NotFound);
    }
}
