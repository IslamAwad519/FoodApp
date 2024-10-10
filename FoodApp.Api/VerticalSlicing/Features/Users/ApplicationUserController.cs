
using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Data.Repository.Specification;
using FoodApp.Api.VerticalSlicing.Features.Users.ChangeUserPassword;
using FoodApp.Api.VerticalSlicing.Features.Users.ChangeUserPassword.Commands;
using FoodApp.Api.VerticalSlicing.Features.Users.DeleteUserProfile.Commands;
using FoodApp.Api.VerticalSlicing.Features.Users.GetAllUsers;
using FoodApp.Api.VerticalSlicing.Features.Users.GetAllUsers.Queries;
using FoodApp.Api.VerticalSlicing.Features.Users.GetUserProfile.Queries;
using FoodApp.Api.VerticalSlicing.Features.Users.UpdateUserProfile;
using FoodApp.Api.VerticalSlicing.Features.Users.UpdateUserProfile.Commands;
using Microsoft.AspNetCore.Mvc;

namespace FoodApp.Api.VerticalSlicing.Features.Users
{

    public class ApplicationUserController : BaseController
    {
        public ApplicationUserController(ControllerParameters controllerParameters) : base(controllerParameters) { }

        [HttpGet("GetAllUsers")]
        public async Task<Result<Pagination<UserResponse>>> GetAllUsers([FromQuery] SpecParams spec)
        {
            var result = await _mediator.Send(new GetAllUsersQuery(spec));
            if (!result.IsSuccess)
            {
                return Result.Failure<Pagination<UserResponse>>(result.Error);
            }

            var UsertCount = await _mediator.Send(new GetUserCountQuery(spec));
            var paginationResult = new Pagination<UserResponse>(spec.PageSize, spec.PageIndex, UsertCount.Data, result.Data);
            return Result.Success(paginationResult);
        }


        [HttpGet("GetUserProfile")]
        public async Task<Result<UserResponse>> GetUserProfile()
        {
            var command = new GetUserProfileQuery();
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpPut("UpdateUserProfile")]
        public async Task<Result<bool>> UpdateUser(UpdateUserRequest viewModel)
        {
            var command = viewModel.Map<UpdateUserProfileCommand>();
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpDelete("DeleteUserProfile")]
        public async Task<Result<bool>> DeleteUser()
        {
            var result = await _mediator.Send(new DeleteUserProfileCommand());
            return result;
        }

        [HttpPost("ChangeUserPassword")]
        public async Task<Result<bool>> ChangePassword(ChangePasswordRequest viewModel)
        {
            var command = viewModel.Map<ChangePasswordCommand>();
            var response = await _mediator.Send(command);
            return response;
        }

    }
}
