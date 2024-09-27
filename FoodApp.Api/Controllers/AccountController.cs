using FoodApp.Api.Abstraction;
using FoodApp.Api.CQRS.Users.Commands;
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
    }
}
