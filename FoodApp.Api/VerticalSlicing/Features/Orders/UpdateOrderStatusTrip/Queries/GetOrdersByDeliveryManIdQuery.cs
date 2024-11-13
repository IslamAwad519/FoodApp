using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Data.Repository.Specification.OrderSpec;
using MediatR;

namespace FoodApp.Api.VerticalSlicing.Features.Orders.UpdateOrderStatusTrip.Queries
{
    public record GetOrdersByDeliveryManIdQuery(int DeliveryManId) : IRequest<Result<List<Order>>>;
    public class GetOrdersByDeliveryManIdQueryHandler : BaseRequestHandler<GetOrdersByDeliveryManIdQuery, Result<List<Order>>>
    {
        public GetOrdersByDeliveryManIdQueryHandler(RequestParameters requestParameters) : base(requestParameters)
        {
        }

        public override async Task<Result<List<Order>>> Handle(GetOrdersByDeliveryManIdQuery request, CancellationToken cancellationToken)
        {
            var orderRepo = _unitOfWork.Repository<Order>();
            var spec = new OrderSpecification(request.DeliveryManId);
            var orders = await orderRepo.ListAsync(spec);


            if (orders == null || !orders.Any())
            {
                return Result.Failure<List<Order>>(OrderErrors.NoOrdersFound);  
            }

            return Result.Success(orders);
        }
    }
}
