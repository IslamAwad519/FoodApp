using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Features.Discounts.AddDiscount;
using FoodApp.Api.VerticalSlicing.Features.Discounts.AddDiscount.Commands;
using FoodApp.Api.VerticalSlicing.Features.Discounts.ApplyDiscount;
using FoodApp.Api.VerticalSlicing.Features.Discounts.ApplyDiscount.Commands;
using FoodApp.Api.VerticalSlicing.Features.Discounts.DeactivateDiscount.Commands;
using FoodApp.Api.VerticalSlicing.Features.Discounts.DeleteDiscount.Commands;
using FoodApp.Api.VerticalSlicing.Features.Discounts.GetActiveDiscounts;
using FoodApp.Api.VerticalSlicing.Features.Discounts.GetActiveDiscounts.Queries;
using FoodApp.Api.VerticalSlicing.Features.Discounts.UpdateDiscount;
using FoodApp.Api.VerticalSlicing.Features.Discounts.UpdateDiscount.Commands;
using FoodApp.Api.VerticalSlicing.Features.Discounts.ViewDiscount;
using FoodApp.Api.VerticalSlicing.Features.Discounts.ViewDiscount.Queries;
using Microsoft.AspNetCore.Mvc;

namespace FoodApp.Api.VerticalSlicing.Features.Discounts
{

    public class DiscountController : BaseController
    {
        public DiscountController(ControllerParameters controllerParameters) : base(controllerParameters) { }

        [HttpPost("AddDiscount")]
        public async Task<Result<int>> AddDiscount(AddDiscountRequest request)
        {
            var command = request.Map<AddDiscountCommand>();
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpDelete("DeleteDiscount/{id}")]
        public async Task<Result<bool>> DeleteDiscount(int id)
        {
            var result = await _mediator.Send(new DeleteDiscountCommand(id));
            return result;
        }

        [HttpPost("UpdateDiscount/{id}")]
        public async Task<Result<bool>> UpdateDiscount(int id, UpdateDiscountRequest request)
        {
            var command = new UpdateDiscountCommand(id, request.DiscountPercent, request.StartDate, request.EndDate);
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpPost("DeactivateDiscount")]
        public async Task<Result<bool>> DeactivateDiscount(int discountId)
        {
            var command = new DeactivateDiscountCommand(discountId);
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpGet("ViewDiscount/{id}")]
        public async Task<Result<ViewDiscountResponse>> GetDiscountById(int id)
        {
            var result = await _mediator.Send(new GetDiscountByIdQuery(id));
            if (!result.IsSuccess)
            {
                return Result.Failure<ViewDiscountResponse>(DiscountErrors.DiscountNotFound);
            }
            var discount = result.Data.Map<ViewDiscountResponse>();
            return Result.Success(discount);
        }

        [HttpGet("GetActiveDiscounts")]
        public async Task<Result<IEnumerable<GetActiveDiscountsResponse>>> GetAllActiveDiscounts()
        {
            var result = await _mediator.Send(new GetAllActiveDiscountsQuery());
            return result;
        }



        [HttpPost("ApplyDiscount")]
        public async Task<Result<decimal>> ApplyDiscount(ApplyDiscountRequest request)
        {
            var command = request.Map<ApplyDiscountCommand>();
            var result = await _mediator.Send(command);
            return result;
        }

    }
}
