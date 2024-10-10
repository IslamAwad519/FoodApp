using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Features.Account;
using FoodApp.Api.VerticalSlicing.Features.Common;
using FoodApp.Api.VerticalSlicing.Features.Roles.AssignRoleToUser.Queries;
using FoodApp.Api.VerticalSlicing.Features.Roles.Common;
using MediatR;

namespace FoodApp.Api.VerticalSlicing.Features.Roles.AssignRoleToUser.Commands
{
    public record AddRoleToUserCommand(int userId, string roleName) : IRequest<Result<bool>>;
    public class AddRoleToUserCommandHandler : BaseRequestHandler<AddRoleToUserCommand, Result<bool>>
    {
        public AddRoleToUserCommandHandler(RequestParameters requestParameters) : base(requestParameters)
        {
        }

        public override async Task<Result<bool>> Handle(AddRoleToUserCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await ValidateUserRole(request.userId, request.roleName);

            if (!validationResult.IsSuccess)
            {
                return Result.Failure<bool>(validationResult.Error);
            }
            var role = validationResult.Data;

            var userRole = new UserRole
            {
                UserId = request.userId,
                RoleId = role.Id
            };

            await _unitOfWork.Repository<UserRole>().AddAsync(userRole);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success(true);
        }
        private async Task<Result<Role>> ValidateUserRole(int userId, string roleName)
        {
            var resultuser = await _mediator.Send(new GetUserByIdQuery(userId));
            if (!resultuser.IsSuccess || resultuser.Data == null)
            {
                return Result.Failure<Role>(UserErrors.UserNotFound);
            }

            var roleResult = await _mediator.Send(new GetRoleByNameQuery(roleName));
            if (!roleResult.IsSuccess || roleResult.Data == null)
            {
                return Result.Failure<Role>(RoleErrors.RoleNotFound);
            }
            var role = roleResult.Data;
            var userRolesResult = await _mediator.Send(new GetUserRoleByIdQuery(userId, role.Id));

            if (userRolesResult.IsSuccess)
            {
                return Result.Failure<Role>(RoleErrors.RoleAlreadyExists);
            }
            return Result.Success(role);
        }
    }
}
