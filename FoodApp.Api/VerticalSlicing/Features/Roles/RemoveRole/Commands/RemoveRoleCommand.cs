using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using MediatR;

namespace FoodApp.Api.VerticalSlicing.Features.Roles.RemoveRole.Commands
{
    public record RemoveRoleCommand(int RoleId) : IRequest<Result<bool>>;
    public class RemoveRoleCommandHandler : BaseRequestHandler<RemoveRoleCommand, Result<bool>>
    {
        public RemoveRoleCommandHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public override async Task<Result<bool>> Handle(RemoveRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _unitOfWork.Repository<Role>().GetByIdAsync(request.RoleId);
            if (role == null)
            {
                return Result.Failure<bool>(RoleErrors.RoleNotFound);
            }

            _unitOfWork.Repository<Role>().Delete(role);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success(true);
        }
    }
}
