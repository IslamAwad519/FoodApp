using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using MediatR;

namespace FoodApp.Api.VerticalSlicing.Features.Orders.UpdateOrderStatus.Queries
{
    public record GetOrderByIdQuery(int OrderId) : IRequest<Result<Order>>;

    public class GetOrderByIdQueryHandler : BaseRequestHandler<GetOrderByIdQuery, Result<Order>>
    {
        public GetOrderByIdQueryHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public override async Task<Result<Order>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Repository<Order>().GetByIdAsync(request.OrderId);
            if (order == null)
            {
                return Result.Failure<Order>(OrderErrors.OrderNotFound);
            }

            return Result.Success(order);
        }
    }
}
