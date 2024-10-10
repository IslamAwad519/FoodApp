using FoodApp.Api.VerticalSlicing.Features.Orders.CreateOrder.DTOs;

namespace FoodApp.Api.VerticalSlicing.Features.Orders.CreateOrder
{
    public class CreateOrderResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public ICollection<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    }
}
