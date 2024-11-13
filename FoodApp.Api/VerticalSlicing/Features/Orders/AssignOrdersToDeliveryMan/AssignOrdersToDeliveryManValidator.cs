using FluentValidation;
using FoodApp.Api.VerticalSlicing.Features.Orders.CreateOrder;

namespace FoodApp.Api.VerticalSlicing.Features.Orders.AssignOrdersToDeliveryMan
{
    public class AssignOrdersToDeliveryManValidator: AbstractValidator<AssignOrdersToDeliveryManRequest>
    {
        public AssignOrdersToDeliveryManValidator()
        {
            RuleFor(x => x.DeliveryManId).NotEmpty().WithMessage("DeliveryMan Id is required");
            RuleFor(x => x.OrderIds).NotEmpty().WithMessage("you should assign the orders");
        }
    }
}
