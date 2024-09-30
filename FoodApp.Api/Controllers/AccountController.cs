using FoodApp.Api.Abstraction;
using FoodApp.Api.CQRS.Users.Commands;
using FoodApp.Api.DTOs;
using FoodApp.Api.Errors;
using FoodApp.Api.Helper;
using FoodApp.Api.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Helper;

namespace FoodApp.Api.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(ControllerParameters controllerParameters) : base(controllerParameters) { }

        [HttpPost("Register")]
        public async Task<Result> Register(RegisterViewModel viewModel)
        {
            var command = viewModel.Map<RegisterCommand>();
            var result= await _mediator.Send(command);
            return result;

        }
        [HttpPost("Login")]
        public async Task<Result<LoginResponse>> Login(LoginViewModel viewModel)
        {
            var command = viewModel.Map<LoginCommand>();
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
            return result;

        }
        [HttpPost("ChangePassword")]
        public async Task<Result<bool>> ChangePassword(ChangePasswordViewModel viewModel)
        {
            var command = viewModel.Map<ChangePasswordCommand>();
            var response = await _mediator.Send(command);
            return response;
        }

        [HttpPost("ForgotPassword")]
        public async Task<Result<bool>> ForgotPassword(ForgotPasswordViewModel viewModel)
        {
            var command = viewModel.Map<ForgotPasswordCommand>();

            var response = await _mediator.Send(command);

            return response;
        }

        [HttpPost("ResetPassword")]
        public async Task<Result<bool>> ResetPassword(ResetPasswordViewModel viewModel)
        {
            var command = viewModel.Map<ResetPasswordCommand>();
            var response = await _mediator.Send(command);

            return response;
        }


    }
}
