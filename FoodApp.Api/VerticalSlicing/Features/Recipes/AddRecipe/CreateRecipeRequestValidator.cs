using FluentValidation;

namespace FoodApp.Api.VerticalSlicing.Features.Recipes.AddRecipe
{
    public class CreateRecipeRequestValidator :AbstractValidator<CreateRecipeRequest>
    {
        public CreateRecipeRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Recipe Name Is Required");

            RuleFor(x => x.Description).NotEmpty().WithMessage("Recipe Description Is Required");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Recipe Price Is Required");
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Recipe Image Is Required");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Recipe Category Is Required");

        }
    }
}
