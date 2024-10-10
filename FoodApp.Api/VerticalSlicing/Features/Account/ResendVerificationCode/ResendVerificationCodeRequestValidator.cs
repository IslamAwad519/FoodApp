using FluentValidation;

namespace FoodApp.Api.VerticalSlicing.Features.Account.ResendVerificationCode
{
    public class ResendVerificationCodeRequestValidator : AbstractValidator<ResendVerificationCodeRequest>
    {
        public ResendVerificationCodeRequestValidator()
        {
            RuleFor(x => x.Email)
               .NotEmpty().WithMessage("Email is required");
        }
    }
}
