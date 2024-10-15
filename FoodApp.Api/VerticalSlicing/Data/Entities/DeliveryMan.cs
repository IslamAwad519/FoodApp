namespace FoodApp.Api.VerticalSlicing.Data.Entities
{
    public class DeliveryMan:BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DeliveryManStatus Status { get; set; } = DeliveryManStatus.Free;
        public ICollection<Order> AssignedOrders { get; set; } = new HashSet<Order>();
        //public int UserId { get; set; }
        //public User User { get; set; }
    }
}
