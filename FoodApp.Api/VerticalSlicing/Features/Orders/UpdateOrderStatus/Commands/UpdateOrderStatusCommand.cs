using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Common.RabbitMQServices;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Features.Account;
using FoodApp.Api.VerticalSlicing.Features.Orders.UpdateOrderStatus.Queries;
using FoodApp.Api.VerticalSlicing.Features.Users.GetUserEmailByUserId.Query;
using MediatR;

namespace FoodApp.Api.VerticalSlicing.Features.Orders.UpdateOrderStatus.Commands
{
    public record UpdateOrderStatusCommand(int OrderId, OrderStatus NewStatus) : IRequest<Result<bool>>;

    public class UpdateOrderStatusCommandHandler : BaseRequestHandler<UpdateOrderStatusCommand, Result<bool>>
    {
        private readonly RabbitMQPublisherService _rabbitMQPublisherService;

        public UpdateOrderStatusCommandHandler(RequestParameters requestParameters, RabbitMQPublisherService rabbitMQPublisherService) : base(requestParameters)
        {
            _rabbitMQPublisherService = rabbitMQPublisherService;
        }

        public override async Task<Result<bool>> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var orderResult = await _mediator.Send(new GetOrderByIdQuery(request.OrderId));
            if (!orderResult.IsSuccess)
            {
                return Result.Failure<bool>(OrderErrors.OrderNotFound);
            }

            var order = orderResult.Data;

            if (order.status == OrderStatus.Pending)
            {
                if (request.NewStatus != OrderStatus.Rejected && request.NewStatus != OrderStatus.Accepted)
                {
                    return Result.Failure<bool>(OrderErrors.OrderNotAcceptedOrRejectedYet);
                }
            }
            else if (order.status == OrderStatus.Accepted)
            { 
                if (request.NewStatus != OrderStatus.InProgress &&
                    request.NewStatus != OrderStatus.Completed &&
                    request.NewStatus != OrderStatus.Delivered)
                {
                    return Result.Failure<bool>(OrderErrors.InvalidStatusUpdate);
                }
            }
            else if (order.status == OrderStatus.Rejected || order.status == OrderStatus.Cancelled)
            {
                return Result.Failure<bool>(OrderErrors.UpdateStatusForInvalidOrder);
            }


            order.status = request.NewStatus;

            _unitOfWork.Repository<Order>().Update(order);
            await _unitOfWork.SaveChangesAsync();
            var emailResult = await _mediator.Send(new GetUserEmailByUserIdQuery(order.UserId));
            if (!emailResult.IsSuccess)
            {
                return Result.Failure<bool>(UserErrors.UserNotFound);
            }
            var updateMessage = new OrderStatusUpdateMessage
            {
                OrderId = request.OrderId,
                NewStatus = request.NewStatus.ToString(),
                UserEmail = emailResult.Data
            };
            _rabbitMQPublisherService.PublishMessage(updateMessage);

            return Result.Success(true);
        }
    }
}
