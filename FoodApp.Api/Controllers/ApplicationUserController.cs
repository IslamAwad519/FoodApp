using FoodApp.Api.Abstraction;
using FoodApp.Api.CQRS.Users.Queries;
using FoodApp.Api.DTOs;
using FoodApp.Api.Helper;
using FoodApp.Api.Repository.Specification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodApp.Api.Controllers
{
   
    public class ApplicationUserController : BaseController
    {
        public ApplicationUserController(ControllerParameters controllerParameters) : base(controllerParameters) { }

        [HttpGet("ListUsers")]
        public async Task<Result<Pagination<UserToReturnDto>>> GetAllUsers([FromQuery] SpecParams spec)
        {
            var result = await _mediator.Send(new GetAllUsersQuery(spec));
            if (!result.IsSuccess)
            {
                return Result.Failure<Pagination<UserToReturnDto>>(result.Error);
            }

            var UsertCount = await _mediator.Send(new GetUserCountQuery(spec));
            var paginationResult = new Pagination<UserToReturnDto>(spec.PageSize, spec.PageIndex, UsertCount.Data, result.Data);
            return Result.Success(paginationResult);
        }

    }
}
