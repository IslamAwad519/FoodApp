namespace FoodApp.Api.VerticalSlicing.Features.Orders.CreateOrder
{
    public class OrderCreatedMessage
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
