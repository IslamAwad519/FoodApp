using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Features.Common;
using FoodApp.Api.VerticalSlicing.Features.Orders.UpdateOrderStatus.Queries;
using MediatR;

namespace FoodApp.Api.VerticalSlicing.Features.Orders.AssignOrdersToDeliveryMan.Commands
{
    public record AssignOrdersToDeliveryManCommand(List<int> OrderIds, int DeliveryManId) : IRequest<Result>;
    public class AssignOrdersToDeliveryManCommandHandler : BaseRequestHandler<AssignOrdersToDeliveryManCommand, Result>
    {
        public AssignOrdersToDeliveryManCommandHandler(RequestParameters requestParameters) : base(requestParameters) {}

        public override async Task<Result> Handle(AssignOrdersToDeliveryManCommand request, CancellationToken cancellationToken)
        {
            var deliveryMan = await _unitOfWork.Repository<DeliveryMan>().GetByIdAsync(request.DeliveryManId);

            if (deliveryMan.Status != DeliveryManStatus.Free)
            {
                return Result.Failure(OrderErrors.UnAvailableDeliveryMAN);
            }
            if (request.OrderIds.Count > 5)
            {
                return Result.Failure(OrderErrors.ExceedOrdersNumber);
            }

            foreach (var orderId in request.OrderIds)
            {
                var orderResult = await _mediator.Send(new GetOrderByIdQuery(orderId));
                if (!orderResult.IsSuccess)
                {
                    return Result.Failure<bool>(OrderErrors.OrderNotFound);
                }
                Order order = orderResult.Data;
                order.DeliveryManId = deliveryMan.Id;
                order.status = OrderStatus.Ready;
                order.StatusTrip = OrderStatusTrip.OnTrip;

                _unitOfWork.Repository<Order>().Update(order);
            }
            deliveryMan.Status = DeliveryManStatus.OnTrip;
            _unitOfWork.Repository<DeliveryMan>().Update(deliveryMan);

            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
