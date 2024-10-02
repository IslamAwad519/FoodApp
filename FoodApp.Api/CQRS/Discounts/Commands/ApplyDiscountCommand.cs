﻿using FoodApp.Api.Abstraction;
using FoodApp.Api.CQRS.Discounts.Queries;
using FoodApp.Api.CQRS.Recipes.Queries;
using FoodApp.Api.Data.Entities;
using FoodApp.Api.DTOs;
using FoodApp.Api.Errors;
using MediatR;
using ProjectManagementSystem.Helper;

namespace FoodApp.Api.CQRS.Discounts.Commands
{
    public record ApplyDiscountCommand(int RecipeId, int DiscountId) : IRequest<Result<decimal>>;

    public class ApplyDiscountCommandHandler : BaseRequestHandler<ApplyDiscountCommand, Result<decimal>>
    {
        public ApplyDiscountCommandHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public async override Task<Result<decimal>> Handle(ApplyDiscountCommand request, CancellationToken cancellationToken)
        {

            var discountResult = await _mediator.Send(new GetDiscountByIdQuery(request.DiscountId));
            if (!discountResult.IsSuccess)
            {
                return Result.Failure<decimal>(DiscountErrors.DiscountNotFound);
            }
            var discount = discountResult.Data.Map<Discount>();


            var recipeResult = await _mediator.Send(new GetRecipeByIdQuery(request.RecipeId));
            if (!recipeResult.IsSuccess)
            {
                return Result.Failure<decimal>(RecipeErrors.RecipeNotFound);
            }
            var recipe = recipeResult.Data;
            var discountedPrice = recipe.Price - (recipe.Price * (discount.DiscountPercent / 100));

            return Result.Success(discountedPrice);
        }
    }

}