using FoodApp.Api.Abstraction;
using FoodApp.Api.CQRS.Users.Commands;
using FoodApp.Api.DTOs;
using FoodApp.Api.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Helper;

namespace FoodApp.Api.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator) 
        {
            _mediator = mediator;
        }


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
