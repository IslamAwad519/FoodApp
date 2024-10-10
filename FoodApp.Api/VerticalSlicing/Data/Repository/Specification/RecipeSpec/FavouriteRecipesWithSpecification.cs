using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Data.Repository.Specification;
using Microsoft.EntityFrameworkCore;

namespace FoodApp.Api.VerticalSlicing.Data.Repository.Specification.RecipeSpec
{
    public class FavouriteRecipesWithSpecification : BaseSpecification<FavouriteRecipe>
    {
        public FavouriteRecipesWithSpecification
            (int userId)
        : base(fr => fr.UserId == userId && fr.IsDeleted == false)
        {
            Includes.Add(fr => fr.Include(fr => fr.Recipe));
        }
    }
}
