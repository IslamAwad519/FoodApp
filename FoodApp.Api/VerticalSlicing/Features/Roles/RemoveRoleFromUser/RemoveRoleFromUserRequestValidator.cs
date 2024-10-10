using FluentValidation;
using FoodApp.Api.VerticalSlicing.Features.Roles.AssignRoleToUser;

namespace FoodApp.Api.VerticalSlicing.Features.Roles.RemoveRoleFromUser
{
    public class RemoveRoleFromUserRequestValidator : AbstractValidator<RemoveRoleFromUserRequest>
    {
        public RemoveRoleFromUserRequestValidator()
        {
            RuleFor(x => x.RoleId)
                 .NotEmpty().WithMessage("RoleName is required");
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId Is required");
        }
    }
}
