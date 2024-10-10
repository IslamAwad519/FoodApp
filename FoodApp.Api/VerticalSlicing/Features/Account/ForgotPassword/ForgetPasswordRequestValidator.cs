using FluentValidation;

namespace FoodApp.Api.VerticalSlicing.Features.Account.ForgotPassword
{
    public class ForgetPasswordRequestValidator :AbstractValidator<ForgetPasswordRequest>
    {
        public ForgetPasswordRequestValidator()
        {
            RuleFor(x => x.Email)
               .NotEmpty().WithMessage("Email is required");
        }
    }

}
