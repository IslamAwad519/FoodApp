namespace FoodApp.Api.VerticalSlicing.Features.Recipes.RateRecipe
{
    public class RateRecipeRequest
    {
        public int RecipeId { get; set; }
        public string? Review { get; set;}
        public int Rating { get; set; }
    }

}
