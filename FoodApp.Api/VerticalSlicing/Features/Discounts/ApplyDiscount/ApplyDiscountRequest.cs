namespace FoodApp.Api.VerticalSlicing.Features.Discounts.ApplyDiscount
{
    public class ApplyDiscountRequest
    {
        public int RecipeId { get; set; }
        public int DiscountId { get; set; }
    }
}
