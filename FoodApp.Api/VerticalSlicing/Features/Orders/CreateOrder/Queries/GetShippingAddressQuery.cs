using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using MediatR;

namespace FoodApp.Api.VerticalSlicing.Features.Orders.CreateOrder.Queries
{
    public record GetShippingAddressQuery(int userId) : IRequest<Result<Address?>>;

    public class GetShippingAddressQueryHandler : BaseRequestHandler<GetShippingAddressQuery, Result<Address?>>
    {

        public GetShippingAddressQueryHandler(RequestParameters requestParameters):base(requestParameters) { }

        public async override Task<Result<Address?>> Handle(GetShippingAddressQuery request, CancellationToken cancellationToken)
        {
            var previousShippingAddresses = await _unitOfWork.Repository<Address>()
                .GetAsync(sa => sa.UserId == request.userId);

            var shippingAddress = previousShippingAddresses.FirstOrDefault();

            return Result.Success(shippingAddress);
        }
    }

}
