using FoodApp.Api.Abstraction;
using FoodApp.Api.Data.Entities;
using FoodApp.Api.DTOs;
using FoodApp.Api.Errors;
using MediatR;
using ProjectManagementSystem.Helper;

namespace FoodApp.Api.CQRS.Discounts.Queries
{
    public record GetDiscountByIdQuery(int DiscountId) : IRequest<Result<Discount>>;

    public class GetDiscountByIdQueryHandler : BaseRequestHandler<GetDiscountByIdQuery, Result<Discount>>
    {
        public GetDiscountByIdQueryHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public async override Task<Result<Discount>> Handle(GetDiscountByIdQuery request, CancellationToken cancellationToken)
        {
            var discountRepo = _unitOfWork.Repository<Discount>();
            var discount = await discountRepo.GetByIdAsync(request.DiscountId);

            if (discount == null)
            {
                return Result.Failure<Discount>(DiscountErrors.DiscountNotFound);
            }
            if (!discount.IsActive)
            {
                return Result.Failure<Discount>(DiscountErrors.DiscoutNotActive); 
            }

            return Result.Success(discount);
        }
    }

}
