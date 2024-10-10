using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Features.Account;
using FoodApp.Api.VerticalSlicing.Features.Common;
using MediatR;

namespace FoodApp.Api.VerticalSlicing.Features.Users.DeleteUserProfile.Commands
{
    public record DeleteUserProfileCommand() : IRequest<Result<bool>>;

    public class DeleteUserProfileCommandHandler : BaseRequestHandler<DeleteUserProfileCommand, Result<bool>>
    {
        public DeleteUserProfileCommandHandler(RequestParameters requestParameters) : base(requestParameters) { }
        public override async Task<Result<bool>> Handle(DeleteUserProfileCommand request, CancellationToken cancellationToken)
        {
            var userId = _userState.ID;
            if (string.IsNullOrEmpty(userId))
            {
                return Result.Failure<bool>(UserErrors.UserNotAuthenticated);
            }

            var userResult = await _mediator.Send(new GetUserByIdQuery(int.Parse(userId)));

            var user = userResult.Data;

            _unitOfWork.Repository<User>().Delete(user);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success(true);
        }
    }
}
