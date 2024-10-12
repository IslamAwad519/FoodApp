namespace FoodApp.Api.VerticalSlicing.Features.Orders.CreateOrder
{
    public class OrderCreatedMessage
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OrderItemDetail> OrderItems { get; set; }
    }

    public class OrderItemDetail
    {
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }
        public int Quantity { get; set; }
    
    }
}
