using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Features.Invoices.GenerateInvoice;
using FoodApp.Api.VerticalSlicing.Features.Invoices.GenerateInvoice.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodApp.Api.VerticalSlicing.Features.Invoices
{
    public class InvoiceController : BaseController
    {
        public InvoiceController(ControllerParameters controllerParameters) : base(controllerParameters) { }


        [HttpPost("CreateInvoice")]
        public async Task<Result<GenerateInvoiceResponse>> CreateInvoice(int orderId)
        {
            var command = new CreateInvoiceCommand(orderId);
            var result = await _mediator.Send(command);
            return result;
        }

    }
}
