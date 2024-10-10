using FluentValidation;

namespace FoodApp.Api.VerticalSlicing.Features.Roles.AssignRoleToUser
{

    public class AssignRoleToUserRequestValidator : AbstractValidator<AssignRoleToUserRequest>
    {
        public AssignRoleToUserRequestValidator()
        {
            RuleFor(x => x.RoleName)
                 .NotEmpty().WithMessage("RoleName is required");
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId Is required");
        }
    }
}
