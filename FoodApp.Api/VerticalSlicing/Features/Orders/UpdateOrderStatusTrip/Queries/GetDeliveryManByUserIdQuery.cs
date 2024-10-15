using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Features.Orders.UpdateOrderStatusTrip.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodApp.Api.VerticalSlicing.Features.Orders.UpdateOrderStatusTrip.Queries
{
    public record GetDeliveryManByUserIdQuery(int UserId) : IRequest<Result<DeliveryMan>>;
    public class GetDeliveryManByUserIdQueryHandler : BaseRequestHandler<GetDeliveryManByUserIdQuery, Result<DeliveryMan>>
    {
        public GetDeliveryManByUserIdQueryHandler(RequestParameters requestParameters) : base(requestParameters)
        {
        }

        public override async Task<Result<DeliveryMan>> Handle(GetDeliveryManByUserIdQuery request, CancellationToken cancellationToken)
        {
            var deliveryMan =await _unitOfWork.Repository<DeliveryMan>()
                      .FirstAsync(dm => dm.UserId == request.UserId);

            if (deliveryMan == null)
            {
                return Result.Failure<DeliveryMan>(OrderErrors.NotFoundDeliveryMan);
            }

            return Result.Success(deliveryMan);
        }
    }
}
