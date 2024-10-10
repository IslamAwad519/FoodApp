using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Features.Orders.UpdateOrderStatus.Queries;
using FoodApp.Api.VerticalSlicing.Features.Orders;
using MediatR;
using FoodApp.Api.VerticalSlicing.Data.Entities;

namespace FoodApp.Api.VerticalSlicing.Features.Invoices.GenerateInvoice.Commands
{
    public record CreateInvoiceCommand(int OrderId) : IRequest<Result<GenerateInvoiceResponse>>;

    public class GenerateInvoiceCommandHandler : BaseRequestHandler<CreateInvoiceCommand, Result<GenerateInvoiceResponse>>
    {
        public GenerateInvoiceCommandHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public override async Task<Result<GenerateInvoiceResponse>> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var orderResult = await _mediator.Send(new GetOrderByIdQuery(request.OrderId));
            if (!orderResult.IsSuccess)
            {
                return Result.Failure<GenerateInvoiceResponse>(OrderErrors.OrderNotFound);
            }

            var order = orderResult.Data;

            var invoice = new Invoice
            {
                OrderId = order.Id,
                TotalPrice = order.TotalPrice
            };

            await _unitOfWork.Repository<Invoice>().AddAsync(invoice);
            await _unitOfWork.SaveChangesAsync();

            var invoiceToReturnDto = invoice.Map<GenerateInvoiceResponse>();

            return Result.Success(invoiceToReturnDto);
        }
    }
}
