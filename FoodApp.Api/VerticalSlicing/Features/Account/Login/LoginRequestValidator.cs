using FluentValidation;

namespace FoodApp.Api.VerticalSlicing.Features.Account.Login
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email)
                 .NotEmpty().WithMessage("Email is required");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required");
        }
    }
}
