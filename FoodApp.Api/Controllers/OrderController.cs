using FoodApp.Api.Abstraction;
using FoodApp.Api.CQRS.Orders.Command;
using FoodApp.Api.DTOs;
using FoodApp.Api.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Helper;

namespace FoodApp.Api.Controllers
{

    public class OrderController : BaseController
    {
        public OrderController(ControllerParameters controllerParameters) : base(controllerParameters) { }

        [HttpPost("CreateOrder")]
        public async Task<Result<OrderToReturnDto>> MakeOrder(CreateOrderViewModel viewModel)
        {
            var command = viewModel.Map<CreateOrderCommand>();
            var result = await _mediator.Send(command);
            return result;
        }
    
    }
}
