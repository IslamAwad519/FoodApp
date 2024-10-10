namespace FoodApp.Api.VerticalSlicing.Features.Discounts.AddDiscount
{
    public class AddDiscountRequest
    {
        public decimal DiscountPercent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
