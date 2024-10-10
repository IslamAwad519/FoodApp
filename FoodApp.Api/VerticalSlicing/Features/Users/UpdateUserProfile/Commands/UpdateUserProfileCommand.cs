using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Features.Account;
using FoodApp.Api.VerticalSlicing.Features.Common;
using MediatR;

namespace FoodApp.Api.VerticalSlicing.Features.Users.UpdateUserProfile.Commands
{
    public record UpdateUserProfileCommand
    (string? UserName,
     string? Email,
     string? Country,
     string? PhoneNumber) : IRequest<Result<bool>>;

    public class UpdateUserProfileCommandHandler : BaseRequestHandler<UpdateUserProfileCommand, Result<bool>>
    {
        public UpdateUserProfileCommandHandler(RequestParameters requestParameters) : base(requestParameters) { }
        public override async Task<Result<bool>> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var userId = _userState.ID;
            if (string.IsNullOrEmpty(userId))
            {
                return Result.Failure<bool>(UserErrors.UserNotAuthenticated);
            }

            var userResult = await _mediator.Send(new GetUserByIdQuery(int.Parse(userId)));

            var user = request.Map(userResult.Data);

            _unitOfWork.Repository<User>().Update(user);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success(true);

        }
    }
}
