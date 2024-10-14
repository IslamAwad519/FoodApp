using System.Net.NetworkInformation;

namespace FoodApp.Api.VerticalSlicing.Data.Entities
{
    public class Order : BaseEntity
    {
        public OrderStatus status { get; set; } = OrderStatus.Pending;
        public decimal TotalPrice { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public Address ShippingAddress { get; set; }
        public int ShippingAddressId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }       
        public int? DeliveryManId { get; set; }
        public DeliveryMan? DeliveryMan { get; set; }
        public OrderStatusTrip? StatusTrip { get; set; }
         
    }
}
