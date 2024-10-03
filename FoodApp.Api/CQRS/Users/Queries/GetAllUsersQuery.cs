using FoodApp.Api.Abstraction;
using FoodApp.Api.Data.Entities;
using FoodApp.Api.DTOs;
using FoodApp.Api.Repository;
using MediatR;
using System.Linq.Expressions;

namespace FoodApp.Api.CQRS.Users.Queries
{
    //public record GetAllUsersQuery :IRequest<Result<UserToReturnDto>>;

    //public class UserToReturnDto
    //{
    //    public string UserName { get; set; }
    //    public string Email { get; set; }
    //    public string PhoneNumber { get; set; }
    //    public string Country { get; set; }
    //    public DateTime DateCreated { get; set; }
    //}
    //public class GetAllUsersQuerHandler :BaseRequestHandler<GetAllUsersQuery, Result<UserToReturnDto>>
    //{
    //    public GetAllUsersQuerHandler(RequestParameters requestParameters) :base(requestParameters) { }

    //    public override Task<Result<UserToReturnDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    //    {
            
    //    }
    //}
}
