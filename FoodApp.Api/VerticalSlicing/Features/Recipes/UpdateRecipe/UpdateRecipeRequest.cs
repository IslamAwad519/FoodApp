namespace FoodApp.Api.VerticalSlicing.Features.Recipes.UpdateRecipe
{

    public record UpdateRecipeRequest(
    int RecipeId,
    string Name,
    IFormFile ImageUrl,
    decimal Price,
    string Description,
    int CategoryId);
}
