using FoodApp.Api.VerticalSlicing.Common.RabbitMQServices;
using FoodApp.Api.VerticalSlicing.Data.Repository.Interface;
using MediatR;

namespace FoodApp.Api.VerticalSlicing.Common
{
    public class RequestParameters
    {
        public EmailSenderHelper EmailSenderHelper { get; set; }
        public IMediator Mediator { get; set; }
        public UserState UserState { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }
        public RabbitMQPublisherService RabbitMQPublisherService { get; set; }

        public RequestParameters(IMediator mediator, UserState userState, IUnitOfWork unitOfWork, EmailSenderHelper emailSenderHelper, RabbitMQPublisherService rabbitMQPublisherService)
        {
            Mediator = mediator;
            UserState = userState;
            UnitOfWork = unitOfWork;
            EmailSenderHelper = emailSenderHelper;
            RabbitMQPublisherService = rabbitMQPublisherService;

        }
    }
}
