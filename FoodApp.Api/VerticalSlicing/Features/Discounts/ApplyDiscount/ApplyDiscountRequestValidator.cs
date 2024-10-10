using FluentValidation;

namespace FoodApp.Api.VerticalSlicing.Features.Discounts.ApplyDiscount
{

    public class ApplyDiscountRequestValidator : AbstractValidator<ApplyDiscountRequest>
    {
        public ApplyDiscountRequestValidator()
        {
            RuleFor(x => x.DiscountId)
                .NotEmpty().WithMessage("DiscountId Is Required");

            RuleFor(x => x.RecipeId)
                .NotEmpty().WithMessage("RecipetId Is Required");
        }
    }
}
