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


    }
}
