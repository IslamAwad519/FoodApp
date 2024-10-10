namespace FoodApp.Api.VerticalSlicing.Features.Discounts.ViewDiscount
{
    public record ViewDiscountResponse
            (int Id,
             decimal DiscountPercent,
             DateTime StartDate, DateTime EndDate,
             DateTime DateCreated);
}
