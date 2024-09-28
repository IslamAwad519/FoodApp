using FoodApp.Api.Abstraction;
using FoodApp.Api.CQRS.Users.Queries;
using FoodApp.Api.Data.Entities;
using FoodApp.Api.DTOs;
using FoodApp.Api.Errors;
using MediatR;

namespace FoodApp.Api.CQRS.Users.Commands
{
    public record VerifyOTPCommand(string Email, string OTP) : IRequest<Result<bool>>;

    public class VerifyOTPCommandHandler : BaseRequestHandler<VerifyOTPCommand, Result<bool>>
    {
        public VerifyOTPCommandHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public override async Task<Result<bool>> Handle(VerifyOTPCommand request, CancellationToken cancellationToken)
        {
            var userResult = await _mediator.Send(new GetUserByEmailQuery(request.Email));

            var user = userResult.Data;

            if (user is null || (user.OTPExpiration is not null && user.OTPExpiration < DateTime.Now) || (user.VerificationOTP is not null && user.VerificationOTP != request.OTP))
            {
                return Result.Failure<bool>(UserErrors.UserNotFound);
            }

            user.VerificationOTP = null;
            user.OTPExpiration = null;
            user.IsEmailVerified = true;

            await _unitOfWork.Repository<User>().SaveChangesAsync();

            return Result.Success(true);
        }
    }
}
