namespace FoodApp.Api.VerticalSlicing.Features.Recipes.AddRecipe
{
    public class CreateRecipeRequest
    {
        public string Name { get; set; }
        public IFormFile ImageUrl { get; set; }
        public string Description { get; set; }
        public  decimal Price { get; set; }
        public int CategoryId { get; set; }

    }

}
