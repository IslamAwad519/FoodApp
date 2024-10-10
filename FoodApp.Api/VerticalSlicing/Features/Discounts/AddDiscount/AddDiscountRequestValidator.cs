using FluentValidation;

namespace FoodApp.Api.VerticalSlicing.Features.Discounts.AddDiscount
{
    public class AddDiscountRequestValidator : AbstractValidator<AddDiscountRequest>
    {
        public AddDiscountRequestValidator()
        {
            RuleFor(x => x.DiscountPercent)
                .NotEmpty().WithMessage("DiscountPercent is required");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("StartDate is required");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("EndDate is required");
        }
    }
}
