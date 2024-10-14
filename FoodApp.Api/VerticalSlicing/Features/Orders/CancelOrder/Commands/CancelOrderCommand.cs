using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Common.RabbitMQServices;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Features.Account;
using FoodApp.Api.VerticalSlicing.Features.Orders.UpdateOrderStatus;
using FoodApp.Api.VerticalSlicing.Features.Orders.UpdateOrderStatus.Queries;
using FoodApp.Api.VerticalSlicing.Features.Users.GetUserEmailByUserId.Query;
using MediatR;

namespace FoodApp.Api.VerticalSlicing.Features.Orders.CancelOrder.Commands
{
    public record CancelOrderCommand(int orderId) :IRequest<Result>;

    public class CancelOrderCommandHandler :BaseRequestHandler<CancelOrderCommand, Result>
    {
        public CancelOrderCommandHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public override async Task<Result> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var orderResult = await _mediator.Send(new GetOrderByIdQuery(request.orderId));
            if(!orderResult.IsSuccess)
            {
                return Result.Failure(OrderErrors.OrderNotFound);
            }

            var order = orderResult.Data;
            if(order.status == OrderStatus.Completed || order.status == OrderStatus.Cancelled || order.status == OrderStatus.Rejected || order.status == OrderStatus.OnTrip)
            {
                return Result.Failure(OrderErrors.OrderCanNotBeCancelled);
            }
           
            order.status = OrderStatus.Cancelled;
            _unitOfWork.Repository<Order>().Update(order);
            await _unitOfWork.SaveChangesAsync();

            var emailResult = await _mediator.Send(new GetUserEmailByUserIdQuery(order.UserId));
            if (!emailResult.IsSuccess)
            {
                return Result.Failure<bool>(UserErrors.UserNotFound);
            }
            var cancelMessage = new OrderStatusUpdateMessage
            {
                OrderId = order.Id,
                NewStatus = order.status.ToString(),
                UserEmail = emailResult.Data
            };
            _rabbitMQPublisherService.PublishMessage(cancelMessage);

            return Result.Success();
        }
    }
}
