namespace FoodApp.Api.VerticalSlicing.Features.Orders.AssignOrdersToDeliveryMan
{
    public class AssignOrdersToDeliveryManRequest
    {
        public List<int> OrderIds { get; set; }
        public int DeliveryManId { get; set; }
    }
}
