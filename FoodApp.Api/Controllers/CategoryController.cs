using FoodApp.Api.Abstraction;
using FoodApp.Api.CQRS.Categories.Commands;
using FoodApp.Api.CQRS.Categories.Queries;
using FoodApp.Api.CQRS.Recipes.Commands;
using FoodApp.Api.Data.Entities;
using FoodApp.Api.DTOs;
using FoodApp.Api.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Helper;

namespace FoodApp.Api.Controllers
{
    public class CategoryController : BaseController
    {
        public CategoryController(ControllerParameters controllerParameters) : base(controllerParameters) { }
        [HttpPost("Add-Category")]
        public async Task<Result<bool>> AddCateory([FromForm] CreateCategoryViewModel viewModel)
        {
            var command = viewModel.Map<CreateCategoryCommand>();
            var response = await _mediator.Send(command);
            return response;
        }
        [HttpPut("Update/{categoryId}")]
        public async Task<Result<bool>> UpdateCategory(int categoryId, [FromBody] CreateCategoryViewModel viewModel)
        {
            var command = new UpdateCategoryCommand(categoryId, viewModel.Name);
            var response = await _mediator.Send(command);
            return response;
        }
        [HttpGet("GetById/{categoryId}")]
        public async Task<Result<Category>> GetCategoryById(int categoryId)
        {
            var response = await _mediator.Send(new GetCategoryByIdQuery(categoryId));
            return response;
        }
        [HttpDelete("Delete/{categoryId}")]
        public async Task<Result<bool>> DeleteCategory(int categoryId)
        {
            var command = new DeleteCategoryCommand(categoryId);
            var response = await _mediator.Send(command);
            return response;
        }

    }
}
