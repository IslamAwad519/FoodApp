using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Features.Recipes;
using FoodApp.Api.VerticalSlicing.Features.Recipes.ViewRecipe.Queries;
using MediatR;

namespace FoodApp.Api.VerticalSlicing.Features.Recipes.UpdateRecipe.Commands;

public record UpdateRecipeCommand(
    int RecipeId,
    string Name,
    IFormFile ImageUrl,
    decimal Price,
    string Description,
    int CategoryId) : IRequest<Result<bool>>;

public class UpdateRecipeCommandHandler : BaseRequestHandler<UpdateRecipeCommand, Result<bool>>
{
    public UpdateRecipeCommandHandler(RequestParameters requestParameters) : base(requestParameters) { }

    public override async Task<Result<bool>> Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipeResult = await _mediator.Send(new GetRecipeByIdQuery(request.RecipeId));
        if (!recipeResult.IsSuccess)
        {
            return Result.Failure<bool>(RecipeErrors.RecipeNotFound);
        }


        var recipe = request.Map(recipeResult.Data);

        _unitOfWork.Repository<Recipe>().Update(recipe);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success(true);
    }
}