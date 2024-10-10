using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Features.Roles.AddRole;
using FoodApp.Api.VerticalSlicing.Features.Roles.AddRole.Commands;
using FoodApp.Api.VerticalSlicing.Features.Roles.AssignRoleToUser;
using FoodApp.Api.VerticalSlicing.Features.Roles.AssignRoleToUser.Commands;
using FoodApp.Api.VerticalSlicing.Features.Roles.GetAllRoles.Queries;
using FoodApp.Api.VerticalSlicing.Features.Roles.RemoveRole.Commands;
using FoodApp.Api.VerticalSlicing.Features.Roles.RemoveRoleFromUser;
using FoodApp.Api.VerticalSlicing.Features.Roles.RemoveRoleFromUser.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodApp.Api.VerticalSlicing.Features.Roles
{
    public class RolesController : BaseController
    {
        public RolesController(ControllerParameters controllerParameters) : base(controllerParameters) { }

        [Authorize]
        [HttpPost("AddRole")]
        public async Task<Result<bool>> AddRoleToUser(AddRoleRequest request)
        {
            var command = request.Map<CreateRoleCommand>();
            var response = await _mediator.Send(command);
            return response;
        }

        [HttpPost("AssignRoleToUser")]
        public async Task<Result<bool>> AssignRoleToUser(AssignRoleToUserRequest request)
        {
            var command = request.Map<AddRoleToUserCommand>();
            var response = await _mediator.Send(command);
            return response;
        }

        [HttpDelete("RemoveRoleFromUser")]
        public async Task<Result<bool>> RemoveRoleFromUser(RemoveRoleFromUserRequest request)
        {
            var command = request.Map<RemoveRoleCommand>();
            var response = await _mediator.Send(command);
            return response;
        }

        [HttpDelete("RemoveRole/{roleId}")]
        public async Task<Result<bool>> RemoveRole(int roleId)
        {
            var response = await _mediator.Send(new RemoveRoleCommand(roleId));
            return response;
        }

        [HttpGet("GetAllRoles")]
        public async Task<Result<List<Role>>> GetAllRoles()
        {
            var response = await _mediator.Send(new GetAllRolesQuery());
            return response;
        }

    }
}
