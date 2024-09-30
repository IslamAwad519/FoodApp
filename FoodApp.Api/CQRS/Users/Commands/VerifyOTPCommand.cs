﻿using FoodApp.Api.Abstraction;
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
            if (user.VerificationOTPExpiration is not null && user.VerificationOTPExpiration < DateTime.Now)
            {
                return Result.Failure<bool>(UserErrors.OTPExpired);
            }

            if (user.VerificationOTP is not null && user.VerificationOTP != request.OTP)
            {
                return Result.Failure<bool>(UserErrors.InvalidOTP);
            }

            user.VerificationOTP = null;
            user.VerificationOTPExpiration = null;
            user.IsEmailVerified = true;

            await _unitOfWork.Repository<User>().SaveChangesAsync();

            return Result.Success(true);
        }
    }
}
