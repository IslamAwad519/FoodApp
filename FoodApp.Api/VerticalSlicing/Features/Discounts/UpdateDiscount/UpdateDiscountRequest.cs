namespace FoodApp.Api.VerticalSlicing.Features.Discounts.UpdateDiscount
{
    public class UpdateDiscountRequest
    {
        public decimal? DiscountPercent { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
