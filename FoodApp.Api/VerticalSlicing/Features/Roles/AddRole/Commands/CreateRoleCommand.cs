using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Features.Roles.Common;
using MediatR;

namespace FoodApp.Api.VerticalSlicing.Features.Roles.AddRole.Commands
{
    public record CreateRoleCommand(string roleName) : IRequest<Result<bool>>;
    public class CreateRoleCommandHandler : BaseRequestHandler<CreateRoleCommand, Result<bool>>
    {
        public CreateRoleCommandHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public override async Task<Result<bool>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var roleResult = await _mediator.Send(new GetRoleByNameQuery(request.roleName), cancellationToken);

            if (roleResult is not null)
            {
                return Result.Failure<bool>(RoleErrors.RoleAlreadyExists);
            }
            var role = request.Map<Role>();
            var roleRepo = _unitOfWork.Repository<Role>();
            await roleRepo.AddAsync(role);
            await roleRepo.SaveChangesAsync();

            return Result.Success(true);

        }
    }
}
