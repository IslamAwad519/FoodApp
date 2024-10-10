using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Features.Account.Login.Queries;
using FoodApp.Api.VerticalSlicing.Features.Common;
using MediatR;

namespace FoodApp.Api.VerticalSlicing.Features.Account.ForgotPassword.Commands

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

            var otpCode = GenerateOTP();
            user.PasswordResetOTP = otpCode;
            user.PasswordResetOTPExpiration = DateTime.Now.AddMinutes(5);

            var userRepo = _unitOfWork.Repository<User>();
            userRepo.Update(user);
            await userRepo.SaveChangesAsync();

            var emailContent = $"Your OTP code to reset your password is: {otpCode}";
            await _emailSenderHelper.SendEmailAsync(request.Email, "Reset Your Password", emailContent);

            return Result.Success(true);
        }

        private string GenerateOTP()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }
}
