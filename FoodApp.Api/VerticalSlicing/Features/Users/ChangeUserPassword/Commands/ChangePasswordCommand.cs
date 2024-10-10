using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Features.Account;
using FoodApp.Api.VerticalSlicing.Features.Common;
using FoodApp.Api.VerticalSlicing.Features.Common.Helper;
using MediatR;

namespace FoodApp.Api.VerticalSlicing.Features.Users.ChangeUserPassword.Commands

{
    public record ChangePasswordCommand(
    string Email,
    string CurrentPassword,
    string NewPassword) : IRequest<Result<bool>>;


    public class ChangePasswordCommandHandler : BaseRequestHandler<ChangePasswordCommand, Result<bool>>
    {

        public ChangePasswordCommandHandler(RequestParameters requestParameters) : base(requestParameters) { }
        public override async Task<Result<bool>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var userResult = await _mediator.Send(new GetUserByEmailQuery(request.Email));

            if (!userResult.IsSuccess)
            {
                return Result.Failure<bool>(UserErrors.UserNotFound);
            }

            var user = userResult.Data;

            if (!PasswordHasher.checkPassword(request.CurrentPassword, user.PasswordHash))
            {
                return Result.Failure<bool>(UserErrors.InvalidCurrentPassword);
            }

            user.PasswordHash = PasswordHasher.HashPassword(request.NewPassword);

            var userRepo = _unitOfWork.Repository<User>();

            userRepo.Update(user);
            await userRepo.SaveChangesAsync();


            return Result.Success(true);
        }

    }
}

