namespace FoodApp.Api.VerticalSlicing.Features.Orders.CreateOrder.DTOs
{
    public class OrderItemDto
    {
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }
        public int Quantity { get; set; }
    }
}
