using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Features.Orders.UpdateOrderStatus.Queries;
using FoodApp.Api.VerticalSlicing.Features.Orders;
using MediatR;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Common.RabbitMQServices;
using FoodApp.Api.VerticalSlicing.Features.Users.GetUserEmailByUserId.Query;

namespace FoodApp.Api.VerticalSlicing.Features.Invoices.GenerateInvoice.Commands
{
    public record CreateInvoiceCommand(int OrderId) : IRequest<Result<GenerateInvoiceResponse>>;

    public class GenerateInvoiceCommandHandler : BaseRequestHandler<CreateInvoiceCommand, Result<GenerateInvoiceResponse>>
    {
        private readonly RabbitMQPublisherService _rabbitMQPublisherService;

        public GenerateInvoiceCommandHandler(RequestParameters requestParameters, RabbitMQPublisherService rabbitMQPublisherService) : base(requestParameters)
        {
            _rabbitMQPublisherService = rabbitMQPublisherService;
        }

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
            var emailResult = await _mediator.Send(new GetUserEmailByUserIdQuery(order.UserId));
            var invoiceMessage = new InvoiceGeneratedMessage
            {
                OrderId = order.Id,
                UserEmail = emailResult.Data,
                TotalAmount = order.TotalPrice,
                GeneratedAt = DateTime.UtcNow
            };
            _rabbitMQPublisherService.PublishInvoiceMessage(invoiceMessage);

            var invoiceToReturnDto = invoice.Map<GenerateInvoiceResponse>();

            return Result.Success(invoiceToReturnDto);
        }
    }
}
