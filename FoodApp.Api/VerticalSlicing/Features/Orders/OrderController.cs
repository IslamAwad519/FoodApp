using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Features.Orders.CreateOrder;
using FoodApp.Api.VerticalSlicing.Features.Orders.CreateOrder.Commands;
using FoodApp.Api.VerticalSlicing.Features.Orders.UpdateOrderStatus;
using FoodApp.Api.VerticalSlicing.Features.Orders.UpdateOrderStatus.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodApp.Api.VerticalSlicing.Features.Orders
{

    public class OrderController : BaseController
    {
        public OrderController(ControllerParameters controllerParameters) : base(controllerParameters) { }

        [HttpPost("CreateOrder")]
        public async Task<Result<CreateOrderResponse>> MakeOrder(CreateOrderRequest request)
        {
            var command = request.Map<CreateOrderCommand>();
            var result = await _mediator.Send(command);
            return result;
        }
        [HttpPost("UpdateOrderStatus")]
        public async Task<Result<bool>> UpdateOrderStatus(UpdateOrderStatusRequest request)
        {
            var command = request.Map<UpdateOrderStatusCommand>();
            var result = await _mediator.Send(command);
            return result;
        }

    }
}
