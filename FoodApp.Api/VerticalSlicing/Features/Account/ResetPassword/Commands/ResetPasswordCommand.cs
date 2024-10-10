using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Features.Account.Login.Queries;
using FoodApp.Api.VerticalSlicing.Features.Common;
using FoodApp.Api.VerticalSlicing.Features.Common.Helper;
using MediatR;

namespace FoodApp.Api.VerticalSlicing.Features.Account.ResetPassword.Commands
{
    public record ResetPasswordCommand(string Email, string OTP, string NewPassword, string ConfirmPassword) : IRequest<Result<bool>>;

    public class ResetPasswordCommandHandler : BaseRequestHandler<ResetPasswordCommand, Result<bool>>
    {
        public ResetPasswordCommandHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public override async Task<Result<bool>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var userResult = await _mediator.Send(new GetUserByEmailQuery(request.Email));
            var user = userResult.Data;

            if (user == null)
                return Result.Failure<bool>(UserErrors.UserNotFound);

            if (user.PasswordResetOTP != request.OTP)
                return Result.Failure<bool>(UserErrors.InvalidResetCode);

            if (user.PasswordResetOTPExpiration is not null && user.PasswordResetOTPExpiration < DateTime.Now)
            {
                return Result.Failure<bool>(UserErrors.OTPExpired);
            }

            if (user.PasswordResetOTP is not null && user.PasswordResetOTP != request.OTP)
            {
                return Result.Failure<bool>(UserErrors.InvalidResetCode);
            }

            if (request.NewPassword != request.ConfirmPassword)
                return Result.Failure<bool>(UserErrors.PasswordsDoNotMatch);

            user.PasswordHash = PasswordHasher.HashPassword(request.NewPassword);

            user.PasswordResetOTP = null;
            user.PasswordResetOTPExpiration = null;

            _unitOfWork.Repository<User>().Update(user);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success(true);
        }
    }
}
