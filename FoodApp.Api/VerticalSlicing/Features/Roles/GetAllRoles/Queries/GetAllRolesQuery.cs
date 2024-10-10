using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using MediatR;

namespace FoodApp.Api.VerticalSlicing.Features.Roles.GetAllRoles.Queries
{
    public record GetAllRolesQuery() : IRequest<Result<List<Role>>>;
    public class GetAllRolesQueryHandler : BaseRequestHandler<GetAllRolesQuery, Result<List<Role>>>
    {
        public GetAllRolesQueryHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public override async Task<Result<List<Role>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _unitOfWork.Repository<Role>().GetAllAsync();
            return Result.Success(roles.ToList());
        }
    }
}
