namespace FoodApp.Api.VerticalSlicing.Features.Orders.UpdateOrderStatus
{
    public class OrderStatusUpdateMessage
    {
        public int OrderId { get; set; }
        public string NewStatus { get; set; }
        public string UserEmail { get; set; }
    }
}
