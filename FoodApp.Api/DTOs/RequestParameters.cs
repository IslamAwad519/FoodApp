using FoodApp.Api.Repository.Interface;
using MediatR;

namespace FoodApp.Api.DTOs
{
    public class RequestParameters
    {
        public IMediator Mediator { get; set; }
        public UserState UserState { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }

        public RequestParameters(IMediator mediator, UserState userState, IUnitOfWork unitOfWork)
        {
            Mediator = mediator;
            UserState = userState;
            UnitOfWork = unitOfWork;
        }
    }
}
