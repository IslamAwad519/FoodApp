using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Data.Repository.Specification;
using FoodApp.Api.VerticalSlicing.Data.Repository.Specification.UsesrSpec;
using FoodApp.Api.VerticalSlicing.Features.Account;
using MediatR;

namespace FoodApp.Api.VerticalSlicing.Features.Users.GetAllUsers.Queries
{
    public record GetAllUsersQuery(SpecParams SpecParams) : IRequest<Result<IEnumerable<UserResponse>>>;

    public class GetAllUsersQuerHandler : BaseRequestHandler<GetAllUsersQuery, Result<IEnumerable<UserResponse>>>
    {
        public GetAllUsersQuerHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public async override Task<Result<IEnumerable<UserResponse>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var spec = new UserSpecification(request.SpecParams);
            var users = await _unitOfWork.Repository<User>().GetAllWithSpecAsync(spec);

            if (users == null)
            {
                return Result.Failure<IEnumerable<UserResponse>>(UserErrors.UserNotFound);
            }

            var mappedUser = users.Map<IEnumerable<UserResponse>>();

            return Result.Success(mappedUser);
        }
    }

}
