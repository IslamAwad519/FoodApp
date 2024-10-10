using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Features.Discounts;
using MediatR;

namespace FoodApp.Api.VerticalSlicing.Features.Discounts.DeactivateDiscount.Commands
{
    public record DeactivateDiscountCommand(int DiscountId) : IRequest<Result<bool>>;

    public class DeactivateDiscountCommandHandler : BaseRequestHandler<DeactivateDiscountCommand, Result<bool>>
    {
        public DeactivateDiscountCommandHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public async override Task<Result<bool>> Handle(DeactivateDiscountCommand request, CancellationToken cancellationToken)
        {
            var discountRepo = _unitOfWork.Repository<Discount>();
            var discount = await discountRepo.GetByIdAsync(request.DiscountId);

            if (discount == null)
            {
                return Result.Failure<bool>(DiscountErrors.DiscountNotFound);
            }

            discount.EndDate = DateTime.UtcNow;

            discountRepo.Update(discount);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success(true);
        }
    }


}
