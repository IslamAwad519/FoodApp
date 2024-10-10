using FluentValidation;

namespace FoodApp.Api.VerticalSlicing.Features.Roles.AddRole
{

    public class AddRoleRequestValidator : AbstractValidator<AddRoleRequest>
    {
        public AddRoleRequestValidator()
        {
            RuleFor(x => x.RoleName)
                 .NotEmpty().WithMessage("RoleName is required");
        }
    }
}
