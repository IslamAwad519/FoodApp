using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Features.Orders.UpdateOrderStatus.Queries;
using MediatR;

namespace FoodApp.Api.VerticalSlicing.Features.Orders.UpdateOrderStatus.Commands
{
    public record UpdateOrderStatusCommand(int OrderId, OrderStatus NewStatus) : IRequest<Result<bool>>;

    public class UpdateOrderStatusCommandHandler : BaseRequestHandler<UpdateOrderStatusCommand, Result<bool>>
    {
        public UpdateOrderStatusCommandHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public override async Task<Result<bool>> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var orderResult = await _mediator.Send(new GetOrderByIdQuery(request.OrderId));
            if (!orderResult.IsSuccess)
            {
                return Result.Failure<bool>(OrderErrors.OrderNotFound);
            }

            var order = orderResult.Data;
            order.status = request.NewStatus;

            _unitOfWork.Repository<Order>().Update(order);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success(true);
        }
    }
}
