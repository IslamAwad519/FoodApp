using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Features.Account;
using FoodApp.Api.VerticalSlicing.Features.Users.GetAllUsers;
using MediatR;

namespace FoodApp.Api.VerticalSlicing.Features.Users.GetUserProfile.Queries
{
    public record GetUserProfileQuery() : IRequest<Result<UserResponse>>;

    public class GetUserProfileQueryHandler : BaseRequestHandler<GetUserProfileQuery, Result<UserResponse>>
    {
        public GetUserProfileQueryHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public async override Task<Result<UserResponse>> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            var userId = _userState.ID;
            if (string.IsNullOrEmpty(userId))
            {
                return Result.Failure<UserResponse>(UserErrors.NoLoggedInUserFound);

            }
            var user = await _unitOfWork.Repository<User>().GetByIdAsync(int.Parse(userId));

            if (user == null)
            {
                return Result.Failure<UserResponse>(UserErrors.UserNotFound);
            }

            var mappedUser = user.Map<UserResponse>();

            return Result.Success(mappedUser);
        }
    }
}
