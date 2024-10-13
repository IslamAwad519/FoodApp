using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Data.Repository.Interface;
using FoodApp.Api.VerticalSlicing.Features.Orders.CreateOrder.DTOs;
using MediatR;

namespace FoodApp.Api.VerticalSlicing.Features.Orders.CreateOrder.Commands
{
    public record CreateShippingAddressCommand(int userId, AddressDto ShippingAddressDto): IRequest<Result<Address>>;

    public class CreateShippingAddressCommandHandler : BaseRequestHandler<CreateShippingAddressCommand, Result<Address>>
    {
        public CreateShippingAddressCommandHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public async override Task<Result<Address>> Handle(CreateShippingAddressCommand request, CancellationToken cancellationToken)
        {

            var shippingAddress = request.ShippingAddressDto.Map<Address>();
            shippingAddress.UserId = request.userId;

            await _unitOfWork.Repository<Address>().AddAsync(shippingAddress);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success(shippingAddress);
        }
    }

}
