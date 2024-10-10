using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using MediatR;

namespace FoodApp.Api.VerticalSlicing.Features.Discounts.GetActiveDiscounts.Queries
{
    public record GetAllActiveDiscountsQuery() : IRequest<Result<IEnumerable<GetActiveDiscountsResponse>>>;

    public class GetAllActiveDiscountsQueryHandler : BaseRequestHandler<GetAllActiveDiscountsQuery, Result<IEnumerable<GetActiveDiscountsResponse>>>
    {
        public GetAllActiveDiscountsQueryHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public async override Task<Result<IEnumerable<GetActiveDiscountsResponse>>> Handle(GetAllActiveDiscountsQuery request, CancellationToken cancellationToken)
        {
            var discountRepo = _unitOfWork.Repository<Discount>();
            var allDiscounts = await discountRepo.GetAllAsync();

            var activeDiscounts = allDiscounts
                .Where(d => d.IsActive)
                .ToList();

            var mappedDiscount = activeDiscounts.Map<IEnumerable<GetActiveDiscountsResponse>>();

            return Result.Success(mappedDiscount);
        }
    }

}
