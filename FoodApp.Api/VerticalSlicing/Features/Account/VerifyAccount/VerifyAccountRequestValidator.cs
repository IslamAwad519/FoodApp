using FluentValidation;

namespace FoodApp.Api.VerticalSlicing.Features.Account.VerifyAccount
{

    public class VerifyAccountRequestValidator : AbstractValidator<VerifyAccountRequest>
    {
        public VerifyAccountRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");

            RuleFor(x => x.OTP).NotEmpty().WithMessage("OTP is required");
        }
    }
}
