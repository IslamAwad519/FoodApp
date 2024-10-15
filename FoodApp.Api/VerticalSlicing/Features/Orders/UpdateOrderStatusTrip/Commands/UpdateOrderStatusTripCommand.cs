using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Features.Account;
using FoodApp.Api.VerticalSlicing.Features.Orders.CreateOrder;
using FoodApp.Api.VerticalSlicing.Features.Orders.UpdateOrderStatus.Queries;
using MassTransit;
using MediatR;

namespace FoodApp.Api.VerticalSlicing.Features.Orders.UpdateOrderStatusTrip.Commands
{
    public record UpdateOrderStatusTripCommand(int OrderId, OrderStatusTrip orderStatusTrip) : IRequest<Result<bool>>;
    public class UpdateOrderStatusTripCommandHandler : BaseRequestHandler<UpdateOrderStatusTripCommand, Result<bool>>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public UpdateOrderStatusTripCommandHandler(RequestParameters requestParameters, IPublishEndpoint publishEndpoint) : base(requestParameters)
        {
            _publishEndpoint = publishEndpoint;
        }

        public override async Task<Result<bool>> Handle(UpdateOrderStatusTripCommand request, CancellationToken cancellationToken)
        {
            var orderResult = await _mediator.Send(new GetOrderByIdQuery(request.OrderId));
            if (!orderResult.IsSuccess)
            {
                return Result.Failure<bool>(OrderErrors.OrderNotFound);
            }
            var order = orderResult.Data;
            if (order.StatusTrip == OrderStatusTrip.Delivered)
            {
                return Result.Failure<bool>(OrderErrors.DeniedAction);
            }
            switch (request.orderStatusTrip)
            {
                case OrderStatusTrip.OnMyWayToCustomer:
                    if (order.StatusTrip != OrderStatusTrip.OnTrip)
                    {
                        return Result.Failure<bool>(OrderErrors.DeniedAction);
                    }
                    break;
                case OrderStatusTrip.ArrivedToCustomer:
                    if (order.StatusTrip != OrderStatusTrip.OnMyWayToCustomer)
                    {
                        return Result.Failure<bool>(OrderErrors.DeniedAction);
                    }
                    break;
                case OrderStatusTrip.Delivered:
                    if (order.StatusTrip != OrderStatusTrip.ArrivedToCustomer)
                    {
                        return Result.Failure<bool>(OrderErrors.DeniedAction);
                    }
                    break;
                default:
                    return Result.Failure<bool>(OrderErrors.DeniedAction);
            }
            order.StatusTrip = request.orderStatusTrip;

            _unitOfWork.Repository<Order>().Update(order);
            await _unitOfWork.SaveChangesAsync();

            await _publishEndpoint.Publish<IOrderStatusTripChangedMessage>(new OrderStatusTripChangedMessage
            {
                OrderId = order.Id,
                NewStatusTrip = request.orderStatusTrip,
                ChangeTime = DateTime.UtcNow,
                UserEmail = order.User.Email
            });
            return Result.Success(true);
        }
    }

}
