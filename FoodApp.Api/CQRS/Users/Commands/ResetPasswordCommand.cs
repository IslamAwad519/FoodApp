using FoodApp.Api.Abstraction;
using FoodApp.Api.CQRS.Users.Queries;
using FoodApp.Api.Data.Entities;
using FoodApp.Api.DTOs;
using FoodApp.Api.Errors;
using FoodApp.Api.Repository.Interface;
using MediatR;
using ProjectManagementSystem.Helper;

namespace FoodApp.Api.CQRS.Users.Commands
{
    public record ResetPasswordCommand(string Email, string ResetCode, string NewPassword) : IRequest<Result<bool>>;

    public class ResetPasswordCommandHandler : BaseRequestHandler<ResetPasswordCommand, Result<bool>>
    {
        public ResetPasswordCommandHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public override async Task<Result<bool>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var userResult = await _mediator.Send(new GetUserByEmailQuery(request.Email));
            var user = userResult.Data;

            if (user == null)
                return Result.Failure<bool>(UserErrors.UserNotFound);

            if (user.PasswordResetCode != request.ResetCode)
                return Result.Failure<bool>(UserErrors.InvalidResetCode);

            user.PasswordHash = PasswordHasher.HashPassword(request.NewPassword);

            user.PasswordResetCode = null;

            _unitOfWork.Repository<User>().Update(user);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success(true);
        }
    }
}
