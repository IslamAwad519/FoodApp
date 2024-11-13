using FoodApp.Api.VerticalSlicing.Common.RabbitMQServices;
using FoodApp.Api.VerticalSlicing.Data.Repository.Interface;
using MediatR;

namespace FoodApp.Api.VerticalSlicing.Common
{
    public abstract class BaseRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        protected readonly IMediator _mediator;
        protected readonly UserState _userState;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly EmailSenderHelper _emailSenderHelper;
        protected readonly RabbitMQPublisherService _rabbitMQPublisherService;

        public BaseRequestHandler(RequestParameters requestParameters)
        {
            _mediator = requestParameters.Mediator;
            _userState = requestParameters.UserState;
            _unitOfWork = requestParameters.UnitOfWork;
            _emailSenderHelper = requestParameters.EmailSenderHelper;
            _rabbitMQPublisherService = requestParameters.RabbitMQPublisherService;
        }
        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);

    }
}
