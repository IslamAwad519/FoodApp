using FoodApp.Api.Abstraction;
using FoodApp.Api.Data.Entities;
using FoodApp.Api.DTOs;
using FoodApp.Api.Errors;
using MediatR;

namespace FoodApp.Api.CQRS.Recipes.Queries;

public record GetRecipeByIdQuery(int RecipeId) : IRequest<Result<Recipe>>;

public class GetRecipeByIdQueryHandler : BaseRequestHandler<GetRecipeByIdQuery, Result<Recipe>>
{
    public GetRecipeByIdQueryHandler(RequestParameters requestParameters) : base(requestParameters) { }

    public override async Task<Result<Recipe>> Handle(GetRecipeByIdQuery request, CancellationToken cancellationToken)
    {
        var recipe = await _unitOfWork.Repository<Recipe>().GetByIdAsync(request.RecipeId);
        if (recipe == null)
        {
            return Result.Failure<Recipe>(RecipeErrors.RecipeNotFound);
        }

        return Result.Success(recipe);
    }
}