using FoodApp.Api.Abstraction;
using FoodApp.Api.CQRS.Recipes.Commands;
using FoodApp.Api.CQRS.Roles.Commands;
using FoodApp.Api.DTOs;
using FoodApp.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Helper;

namespace FoodApp.Api.Controllers;

public class RecipesController : BaseController
{
    public RecipesController(ControllerParameters controllerParameters) : base(controllerParameters) { }

    //[Authorize]
    [HttpPost("Add-Recipe")]
    public async Task<Result<bool>> AddRecipe([FromForm] CreateRecipeViewModel viewModel)
    {
        var command = viewModel.Map<CreateRecipeCommand>();
        var response = await _mediator.Send(command);
        return response;
    }

    //[Authorize]
    [HttpPost("Update-Recipe")]
    public async Task<Result<bool>> UpdateRecipe([FromForm] UpdateRecipeViewModel viewModel)
    {
        var command = viewModel.Map<UpdateRecipeCommand>();
        var response = await _mediator.Send(command);
        return response;
    }

}
