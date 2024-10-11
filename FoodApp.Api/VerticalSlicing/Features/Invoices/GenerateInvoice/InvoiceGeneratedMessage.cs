namespace FoodApp.Api.VerticalSlicing.Features.Invoices.GenerateInvoice
{
    public class InvoiceGeneratedMessage
    {
        public int OrderId { get; set; }
        public string UserEmail { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime GeneratedAt { get; set; }
    }
}
