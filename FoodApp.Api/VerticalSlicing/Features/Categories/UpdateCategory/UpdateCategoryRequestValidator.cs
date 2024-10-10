using FluentValidation;

namespace FoodApp.Api.VerticalSlicing.Features.Categories.UpdateCategory
{
    public class UpdateCategoryRequestValidator :AbstractValidator<UpdateCategoryRequest>
    {
        public UpdateCategoryRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Category name is required");
        }
    }
}
