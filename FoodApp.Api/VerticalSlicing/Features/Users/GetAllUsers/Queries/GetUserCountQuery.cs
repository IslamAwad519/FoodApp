using MediatR;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Data.Repository.Specification;
using FoodApp.Api.VerticalSlicing.Data.Repository.Specification.UsesrSpec;

namespace FoodApp.Api.VerticalSlicing.Features.Users.GetAllUsers.Queries
{
    public record GetUserCountQuery(SpecParams SpecParams) : IRequest<Result<int>>;

    public class GetUserCountQueryHandler : BaseRequestHandler<GetUserCountQuery, Result<int>>
    {
        public GetUserCountQueryHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public async override Task<Result<int>> Handle(GetUserCountQuery request, CancellationToken cancellationToken)
        {
            var userSpec = new CountUserWithSpec(request.SpecParams);
            var count = await _unitOfWork.Repository<User>().GetCountWithSpecAsync(userSpec);

            return Result.Success(count);
        }
    }
}
