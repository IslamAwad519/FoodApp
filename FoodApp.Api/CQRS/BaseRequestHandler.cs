﻿using FoodApp.Api.DTOs;
using FoodApp.Api.Repository.Interface;
using MediatR;

namespace FoodApp.Api.CQRS
{
    public abstract class BaseRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        protected readonly IMediator _mediator;
        protected readonly UserState _userState;
        protected readonly IUnitOfWork _unitOfWork;

        public BaseRequestHandler(RequestParameters requestParameters)
        {
             _mediator = requestParameters.Mediator;
            _userState = requestParameters.UserState;
            _unitOfWork = requestParameters.UnitOfWork;
        }
        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
       
    }
}
