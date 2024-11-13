using FluentValidation;

namespace FoodApp.Api.VerticalSlicing.Features.Orders.CreateOrder
{
    public class CreateOrderRequestValidator :AbstractValidator<CreateOrderRequest>
    {
        public CreateOrderRequestValidator()
        {
            RuleFor(x => x.OrderItems.Select(x => x.RecipeId)).NotEmpty().WithMessage("RecipeId is required");
            RuleFor(x => x.OrderItems.Select(x => x.Quantity)).NotEmpty().WithMessage("Quantity is required");

            //RuleFor(x => x.ShippingAddress.FirstName).NotEmpty().WithMessage("FirstName is required");
            //RuleFor(x => x.ShippingAddress.LastName).NotEmpty().WithMessage("LastName is required");
            //RuleFor(x => x.ShippingAddress.City).NotEmpty().WithMessage("City is required");
            //RuleFor(x => x.ShippingAddress.Country).NotEmpty().WithMessage("Country is required");
            //RuleFor(x => x.ShippingAddress.Street).NotEmpty().WithMessage("Street is required");




        }
    }
}
