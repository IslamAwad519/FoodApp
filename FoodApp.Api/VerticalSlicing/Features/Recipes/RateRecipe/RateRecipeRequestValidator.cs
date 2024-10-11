using FluentValidation;

namespace FoodApp.Api.VerticalSlicing.Features.Recipes.RateRecipe
{
    public class RateRecipeRequestValidator :AbstractValidator<RateRecipeRequest>
    {
        public RateRecipeRequestValidator()
        {
            RuleFor(x=>x.RecipeId).NotEmpty().WithMessage("RecipeId is required");
            RuleFor(x => x.Rating).NotEmpty().WithMessage("rating is required");

        }
    }
}
