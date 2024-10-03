using FoodApp.Api.Abstraction;
using FoodApp.Api.CQRS.Recipes.Commands;
using FoodApp.Api.CQRS.Recipes.Queries;
using FoodApp.Api.CQRS.Roles.Commands;
using FoodApp.Api.Data.Entities.RecipeEntity;
using FoodApp.Api.DTOs;
using FoodApp.Api.Helper;
using FoodApp.Api.Repository.Specification;
using FoodApp.Api.Response;
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
    [HttpPut("Update-Recipe")]
    public async Task<Result<bool>> UpdateRecipe([FromForm] UpdateRecipeViewModel viewModel)
    {
        var command = viewModel.Map<UpdateRecipeCommand>();
        var response = await _mediator.Send(command);
        return response;
    }

    //[Authorize]
    [HttpDelete("Delete-Recipe")]
    public async Task<Result<bool>> DeleteRecipe([FromForm] int RecipeId)
    {
        var command = new DeleteRecipeCommand(RecipeId);
        var response = await _mediator.Send(command);
        return response;
    }

    //[Authorize]
    [HttpGet("View-Recipe/{RecipeId}")]
    public async Task<Result<RecipeResponse>> GetRecipeById( int RecipeId)
    {
        var command = new GetRecipeByIdQuery(RecipeId);
        var result = await _mediator.Send(command);
        if (!result.IsSuccess)
        {
            return Result.Failure<RecipeResponse>(result.Error);
        }
        var recipe = result.Data;

        var response = recipe.Map<RecipeResponse>();

        return Result.Success(response);
    }

    //[Authorize]
    [HttpGet("List-Recipes")]
    public async Task<Result<Pagination<RecipeResponse>>> GetAllRecipes([FromQuery] SpecParams spec)
    {
        var result = await _mediator.Send(new GetRecipesQuery(spec));
        if (!result.IsSuccess)
        {
            return Result.Failure<Pagination<RecipeResponse>>(result.Error);
        }

        var RecipesCount = await _mediator.Send(new GetRecipesCountQuery(spec));
        var paginationResult = new Pagination<RecipeResponse>(spec.PageSize, spec.PageIndex, RecipesCount.Data, result.Data);

        return Result.Success(paginationResult);
    }

}
