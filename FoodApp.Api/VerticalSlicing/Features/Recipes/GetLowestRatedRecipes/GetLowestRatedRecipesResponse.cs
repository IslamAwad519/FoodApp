namespace FoodApp.Api.VerticalSlicing.Features.Recipes.GetLowestRatedRecipes
{
    public class GetLowestRatedRecipesResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public decimal Price { get; set; }
        public string Description { get; set; } = null!;
        public decimal? Discount { get; set; }
        public string CategoryName { get; set; } = null!;
    }

}
