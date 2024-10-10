namespace FoodApp.Api.VerticalSlicing.Data.Entities
{
    public class Invoice : BaseEntity
    {
        public Order Order { get; set; }
        public int OrderId { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
