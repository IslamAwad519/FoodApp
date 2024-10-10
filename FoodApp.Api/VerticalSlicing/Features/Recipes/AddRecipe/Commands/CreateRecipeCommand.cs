using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Features.Recipes;
using MediatR;

namespace FoodApp.Api.VerticalSlicing.Features.Recipes.AddRecipe.Commands;

public record CreateRecipeCommand(
    string Name,
    IFormFile ImageUrl,
    decimal Price,
    string Description,
    int CategoryId) : IRequest<Result<bool>>;

public class CreateRecipeCommandHandler : BaseRequestHandler<CreateRecipeCommand, Result<bool>>
{
    private readonly ILogger<CreateRecipeCommandHandler> _logger;

    public CreateRecipeCommandHandler(
        RequestParameters requestParameters,
        ILogger<CreateRecipeCommandHandler> logger) : base(requestParameters)
    {
        _logger = logger;
    }

    public override async Task<Result<bool>> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var recipe = request.Map<Recipe>();
            _logger.LogInformation("Mapped recipe object for {RecipeName}", request.Name);

            await _unitOfWork.Repository<Recipe>().AddAsync(recipe);

            await _unitOfWork.SaveChangesAsync();

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the recipe: {RecipeName}", request.Name);
            return Result.Failure<bool>(RecipeErrors.RecipeNotCreated);
        }
    }
}