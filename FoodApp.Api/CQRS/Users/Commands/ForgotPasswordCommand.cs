using FoodApp.Api.Abstraction;
using FoodApp.Api.CQRS.Users.Queries;
using FoodApp.Api.Data.Entities;
using FoodApp.Api.DTOs;
using FoodApp.Api.Errors;
using FoodApp.Api.Helper;
using MediatR;

namespace FoodApp.Api.CQRS.Users.Commands
{
    public record ForgotPasswordCommand(string Email) : IRequest<Result<bool>>;
    public class ForgotPasswordCommandHandler : BaseRequestHandler<ForgotPasswordCommand, Result<bool>>
    {
        public ForgotPasswordCommandHandler(RequestParameters requestParameters) : base(requestParameters) { }
        public override async Task<Result<bool>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = (await _mediator.Send(new GetUserByEmailQuery(request.Email))).Data;

            if (user == null)
                return Result.Failure<bool>(UserErrors.UserNotFound);

            //if (!user.IsEmailVerified)
            //    return Result.Failure<bool>(UserErrors.UserNotVerified);


            var resetCode = Guid.NewGuid().ToString();
            user.PasswordResetCode = resetCode;
            await _unitOfWork.Repository<User>().SaveChangesAsync();

            var resetUrl = $"https://localhost:7120/api/Account/reset-password?email={request.Email}&code={resetCode}";

            await _emailSenderHelper.SendEmailAsync(request.Email, "Reset Your Password", $"Please reset your password by clicking the link: <a href='{resetUrl}'>Reset Password</a>");

            return Result.Success(true);
        }
    }
}
