﻿using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Data.Repository.Specification;
using FoodApp.Api.VerticalSlicing.Features.Recipes.AddRecipe;
using FoodApp.Api.VerticalSlicing.Features.Recipes.AddRecipe.Commands;
using FoodApp.Api.VerticalSlicing.Features.Recipes.AddRecipeToFavourite;
using FoodApp.Api.VerticalSlicing.Features.Recipes.AddRecipeToFavourite.Commands;
using FoodApp.Api.VerticalSlicing.Features.Recipes.DeleteRecipe.Commands;
using FoodApp.Api.VerticalSlicing.Features.Recipes.ListRecipes;
using FoodApp.Api.VerticalSlicing.Features.Recipes.ListRecipes.Queries;
using FoodApp.Api.VerticalSlicing.Features.Recipes.RemoveRecipeFromFavourite.Commands;
using FoodApp.Api.VerticalSlicing.Features.Recipes.UpdateRecipe;
using FoodApp.Api.VerticalSlicing.Features.Recipes.UpdateRecipe.Commands;
using FoodApp.Api.VerticalSlicing.Features.Recipes.ViewFavouriteRecipes.Queries;
using FoodApp.Api.VerticalSlicing.Features.Recipes.ViewRecipe.Queries;
using Microsoft.AspNetCore.Mvc;

namespace FoodApp.Api.VerticalSlicing.Features.Recipes;

public class RecipesController : BaseController
{

    public RecipesController(ControllerParameters controllerParameters) : base(controllerParameters) { }

    //[Authorize]
    [HttpPost("AddRecipe")]
    public async Task<Result<bool>> AddRecipe([FromForm] CreateRecipeRequest request)
    {
        var command = request.Map<CreateRecipeCommand>();
        var response = await _mediator.Send(command);
        return response;
    }

    //[Authorize]
    [HttpPut("UpdateRecipe")]
    public async Task<Result<bool>> UpdateRecipe([FromForm] UpdateRecipeRequest request)
    {
        var command = request.Map<UpdateRecipeCommand>();
        var response = await _mediator.Send(command);
        return response;
    }

    //[Authorize]
    [HttpDelete("DeleteRecipe")]
    public async Task<Result<bool>> DeleteRecipe(int RecipeId)
    {
        var command = new DeleteRecipeCommand(RecipeId);
        var response = await _mediator.Send(command);
        return response;
    }

    //[Authorize]
    [HttpGet("ViewRecipe/{RecipeId}")]
    public async Task<Result<ListRecipesResponse>> GetRecipeById(int RecipeId)
    {
        var command = new GetRecipeByIdQuery(RecipeId);
        var result = await _mediator.Send(command);
        if (!result.IsSuccess)
        {
            return Result.Failure<ListRecipesResponse>(RecipeErrors.RecipeNotFound);
        }
        var recipe = result.Data;

        var response = recipe.Map<ListRecipesResponse>();

        return Result.Success(response);
    }

    //[Authorize]
    [HttpGet("ListRecipes")]
    public async Task<Result<Pagination<ListRecipesResponse>>> GetAllRecipes([FromQuery] SpecParams spec)
    {
        var result = await _mediator.Send(new GetRecipesQuery(spec));
        if (!result.IsSuccess)
        {
            return Result.Failure<Pagination<ListRecipesResponse>>(result.Error);
        }

        var RecipesCount = await _mediator.Send(new GetRecipesCountQuery(spec));
        var paginationResult = new Pagination<ListRecipesResponse>(spec.PageSize, spec.PageIndex, RecipesCount.Data, result.Data);

        return Result.Success(paginationResult);
    }
    [HttpPost("AddRecipeToFavourite")]
    public async Task<Result<bool>> AddRecipeToFavourite([FromForm] AddRecipeToFavoriteRequest request)
    {
        var command = request.Map<AddRecipeToFavouritesCommand>();
        var response = await _mediator.Send(command);
        return response;
    }
    [HttpDelete("RemoveRecipeFromFavourite/{RecipeId}")]
    public async Task<Result<bool>> RemoveRecipeFromFavourite(int RecipeId)
    {
        var response = await _mediator.Send(new RemoveRecipeFromFavouritesCommand(RecipeId));
        return response;
    }
    [HttpGet("ViewFavouriteRecipes")]
    public async Task<IActionResult> ViewFavouriteRecipes()
    {
        var result = await _mediator.Send(new GetFavouriteRecipesForLoggedUserQuery());

        if (!result.IsSuccess)
        {
            return NotFound(result.Error);
        }

        return Ok(Result.Success(result.Data));
    }

}
