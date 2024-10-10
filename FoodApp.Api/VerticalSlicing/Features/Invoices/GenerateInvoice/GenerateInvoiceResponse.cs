namespace FoodApp.Api.VerticalSlicing.Features.Invoices.GenerateInvoice
{
    public class GenerateInvoiceResponse
    {
        public int OrderId { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
