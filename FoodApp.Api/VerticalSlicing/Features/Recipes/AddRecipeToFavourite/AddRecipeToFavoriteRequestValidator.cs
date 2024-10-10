using FluentValidation;

namespace FoodApp.Api.VerticalSlicing.Features.Recipes.AddRecipeToFavourite
{
    public class AddRecipeToFavoriteRequestValidator :AbstractValidator<AddRecipeToFavoriteRequest>
    {
        public AddRecipeToFavoriteRequestValidator()
        {
            RuleFor(x=>x.RecipeId).NotEmpty().WithMessage("RecipeId is required");
        }
    }

}
