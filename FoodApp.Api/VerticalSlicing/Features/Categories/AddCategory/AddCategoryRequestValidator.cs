using FluentValidation;

namespace FoodApp.Api.VerticalSlicing.Features.Categories.AddCategory
{
    public class AddCategoryRequestValidator :AbstractValidator<AddCategoryRequest>
    {
        public AddCategoryRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Category name is required");
        }
    }
}
