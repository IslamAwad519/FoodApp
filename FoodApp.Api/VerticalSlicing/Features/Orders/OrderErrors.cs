using FoodApp.Api.VerticalSlicing.Features.Common;
using System.Net.NetworkInformation;

namespace FoodApp.Api.VerticalSlicing.Features.Orders
{
    public class OrderErrors
    {
        public static readonly Error OrderNotFound =
             new("Order not found.", StatusCodes.Status404NotFound);

        public static readonly Error OrderNotAcceptedOrRejectedYet =
             new("The order must be accepted or rejected before status updates.", StatusCodes.Status400BadRequest);

        public static readonly Error InvalidStatusUpdate =
            new("You can only set the status to InProgress, Completed or Delivered.", StatusCodes.Status400BadRequest);

        public static readonly Error UpdateStatusForInvalidOrder =
            new("Cannot update the status of a cancelled or rejected order", StatusCodes.Status400BadRequest);

        public static readonly Error InvalidRating =
            new("Rating must be between 1 and 5", StatusCodes.Status400BadRequest);

        public static readonly Error OrderCanNotBeCancelled =
            new("Order can not be cancelled", StatusCodes.Status400BadRequest);
        public static readonly Error UnAvailableDeliveryMAN =
            new("the delivery Man is not Available", StatusCodes.Status400BadRequest);

        public static readonly Error ExceedOrdersNumber =
           new("You can't assign more than 5 orders to deliverMan at a time", StatusCodes.Status400BadRequest);


        public static readonly Error ShippingAddressRequired =
            new("Shipping Address Required", StatusCodes.Status400BadRequest);

        public static readonly Error NoOrdersFound =
            new("No Orders Found for this user", StatusCodes.Status400BadRequest);

        public static readonly Error FailedToRetrieveShippingAddress =
              new("Failed To Retrieve ShippingAddress of this user", StatusCodes.Status400BadRequest);

        public readonly Error NoShippingAddressFound =
              new("No Shipping Address Found for this user", StatusCodes.Status400BadRequest);

        public static readonly Error DeniedAction =
             new("Incorrect Update Status", StatusCodes.Status400BadRequest);

        public static readonly Error NotFoundDeliveryMan =
             new("You are not authorized to do this action", StatusCodes.Status400BadRequest);
    }
}
