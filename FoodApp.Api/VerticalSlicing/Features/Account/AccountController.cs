using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Features.Account.ForgotPassword;
using FoodApp.Api.VerticalSlicing.Features.Account.ForgotPassword.Commands;
using FoodApp.Api.VerticalSlicing.Features.Account.Login;
using FoodApp.Api.VerticalSlicing.Features.Account.Login.Commands;
using FoodApp.Api.VerticalSlicing.Features.Account.RefreshTokens.Commands;
using FoodApp.Api.VerticalSlicing.Features.Account.Register;
using FoodApp.Api.VerticalSlicing.Features.Account.Register.Orchestrator;
using FoodApp.Api.VerticalSlicing.Features.Account.ResendVerificationCode;
using FoodApp.Api.VerticalSlicing.Features.Account.ResendVerificationCode.Commands;
using FoodApp.Api.VerticalSlicing.Features.Account.ResetPassword;
using FoodApp.Api.VerticalSlicing.Features.Account.ResetPassword.Commands;
using FoodApp.Api.VerticalSlicing.Features.Account.RevokeToken.Commands;
using FoodApp.Api.VerticalSlicing.Features.Account.VerifyAccount;
using FoodApp.Api.VerticalSlicing.Features.Account.VerifyAccount.Commands;
using Microsoft.AspNetCore.Mvc;

namespace FoodApp.Api.VerticalSlicing.Features.Account
{
    public class AccountController : BaseController
    {
        public AccountController(ControllerParameters controllerParameters) : base(controllerParameters) { }

        [HttpPost("Register")]
        public async Task<Result> Register(RegisterRequest viewModel)
        {
            var command = viewModel.Map<RegisterOrchestrator>();
            var result = await _mediator.Send(command);
            return result;

        }

        [HttpPost("VerifyAccount")]
        public async Task<Result<bool>> Verify(VerifyAccountRequest request)
        {
            var command = request.Map<VerifyOTPCommand>();
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpPost("ResendVerificationCode")]
        public async Task<Result<bool>> ResendVerificationCode(ResendVerificationCodeRequest requset)
        {
            var command = requset.Map<SendVerificationOTP>();
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpPost("Login")]
        public async Task<Result<LoginResponse>> Login(LoginRequest request)
        {
            var command = request.Map<LoginCommand>();
            var result = await _mediator.Send(command);
            if (result.Data == null || string.IsNullOrEmpty(result.Data.RefreshToken))
            {
                return Result.Failure<LoginResponse>(UserErrors.InvalidCredentials);
            }
            CookieHelper.SetRefreshTokenCookie(Response, result.Data.RefreshToken);
            return result;

        }

        [HttpPost("RefreshToken")]
        public async Task<Result<LoginResponse>> RefreshToken()
        {
            var refreshToken = CookieHelper.GetRefreshTokenCookie(Request);
            var result = await _mediator.Send(new RefreshTokenCommand(refreshToken));
            if (!result.IsSuccess)
                return Result.Failure<LoginResponse>(UserErrors.InvalidRefreshToken);
            CookieHelper.SetRefreshTokenCookie(Response, result.Data.RefreshToken);

            return result;

        }

        [HttpPost("RevokeToken")]
        public async Task<Result<bool>> RevokeToken(string? token)
        {
            var result = await _mediator.Send(new RevokeTokenCommand(token ?? Request.Cookies["refreshToken"]));
            if (string.IsNullOrEmpty(token))
                return Result.Failure<bool>(UserErrors.TokenIsRequired);
            return result;
        }


        [HttpPost("ForgotPassword")]
        public async Task<Result<bool>> ForgotPassword(ForgetPasswordRequest request)
        {
            var command = request.Map<ForgotPasswordCommand>();

            var response = await _mediator.Send(command);

            return response;
        }


        [HttpPost("ResetPassword")]
        public async Task<Result<bool>> ResetPassword(ResetPasswordRequest request)
        {
            var command = request.Map<ResetPasswordCommand>();
            var response = await _mediator.Send(command);

            return response;
        }


    }
}
