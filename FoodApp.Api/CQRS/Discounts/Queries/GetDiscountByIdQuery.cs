using FoodApp.Api.Abstraction;
using FoodApp.Api.Data.Entities;
using FoodApp.Api.DTOs;
using FoodApp.Api.Errors;
using MediatR;
using ProjectManagementSystem.Helper;

namespace FoodApp.Api.CQRS.Discounts.Queries
{
    public record GetDiscountByIdQuery(int DiscountId) : IRequest<Result<DiscountToReturnDto>>;

    public class GetDiscountByIdQueryHandler : BaseRequestHandler<GetDiscountByIdQuery, Result<DiscountToReturnDto>>
    {
        public GetDiscountByIdQueryHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public async override Task<Result<DiscountToReturnDto>> Handle(GetDiscountByIdQuery request, CancellationToken cancellationToken)
        {
            var discountRepo = _unitOfWork.Repository<Discount>();
            var discount = await discountRepo.GetByIdAsync(request.DiscountId);

            if (discount == null)
            {
                return Result.Failure<DiscountToReturnDto>(DiscountErrors.DiscountNotFound);
            }
            if (!discount.IsActive)
            {
                return Result.Failure<DiscountToReturnDto>(DiscountErrors.DiscoutNotActive); 
            }

            var mappedDiscount = discount.Map<DiscountToReturnDto>();
            return Result.Success(mappedDiscount);
        }
    }

}
