using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Data.Repository.Specification.OrderSpec;
using FoodApp.Api.VerticalSlicing.Features.Orders.UpdateOrderStatus.Queries;
using MediatR;

namespace FoodApp.Api.VerticalSlicing.Features.Orders.UpdateOrderStatusTrip.Queries
{
    public record GetOrderWithUserByIdQuery(int OrderId) : IRequest<Result<Order>>;

    public class GetOrderWithUserByIdQueryHandler : BaseRequestHandler<GetOrderWithUserByIdQuery, Result<Order>>
    {
        public GetOrderWithUserByIdQueryHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public override async Task<Result<Order>> Handle(GetOrderWithUserByIdQuery request, CancellationToken cancellationToken)
        {
            var spec = new OrderWithUserSpecification(request.OrderId);
            var order = await _unitOfWork.Repository<Order>().GetByIdWithSpecAsync(spec);
            if (order == null)
            {
                return Result.Failure<Order>(OrderErrors.OrderNotFound);
            }

            return Result.Success(order);
        }
    }

}
