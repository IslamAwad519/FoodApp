using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Features.Categories.AddCategory;
using FoodApp.Api.VerticalSlicing.Features.Categories.AddCategory.Commands;
using FoodApp.Api.VerticalSlicing.Features.Categories.DeleteCategory.Commands;
using FoodApp.Api.VerticalSlicing.Features.Categories.UpdateCategory;
using FoodApp.Api.VerticalSlicing.Features.Categories.UpdateCategory.Commands;
using FoodApp.Api.VerticalSlicing.Features.Categories.ViewCategory;
using FoodApp.Api.VerticalSlicing.Features.Categories.ViewCategory.Queries;
using Microsoft.AspNetCore.Mvc;

namespace FoodApp.Api.VerticalSlicing.Features.Categories
{
    public class CategoryController : BaseController
    {
        public CategoryController(ControllerParameters controllerParameters) : base(controllerParameters) { }


        [HttpPost("AddCategory")]
        public async Task<Result<int>> AddCateory(AddCategoryRequest request)
        {
            var command = request.Map<CreateCategoryCommand>();
            var response = await _mediator.Send(command);
            return response;
        }

        [HttpPut("UpdateCategory/{categoryId}")]
        public async Task<Result<bool>> UpdateCategory(int categoryId, UpdateCategoryRequest request)
        {
            var command = new UpdateCategoryCommand(categoryId, request.Name);
            var response = await _mediator.Send(command);
            return response;
        }


        [HttpGet("ViewCategory/{categoryId}")]
        public async Task<Result<ViewCategoryResponse>> GetCategoryById(int categoryId)
        {
            var result = await _mediator.Send(new GetCategoryByIdQuery(categoryId));
            if (!result.IsSuccess)
            {
                return Result.Failure<ViewCategoryResponse>(CategoryErrors.CategoryNotFound);
            }
            var category = result.Data.Map<ViewCategoryResponse>();
            return Result.Success(category);
        }


        [HttpDelete("DeleteCategory/{categoryId}")]
        public async Task<Result<bool>> DeleteCategory(int categoryId)
        {
            var command = new DeleteCategoryCommand(categoryId);
            var response = await _mediator.Send(command);
            return response;
        }

    }
}
