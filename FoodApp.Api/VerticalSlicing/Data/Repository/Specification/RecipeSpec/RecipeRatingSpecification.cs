using FoodApp.Api.VerticalSlicing.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodApp.Api.VerticalSlicing.Data.Repository.Specification.RecipeSpec
{
    public class RecipeRatingSpecification : BaseSpecification<Recipe>
    {
        public RecipeRatingSpecification(bool topRated)
            :base(r => r.RecipeRatings.Any())
        {
            Includes.Add(r => r.Include(r => r.RecipeRatings));
            Includes.Add(r => r.Include(r => r.RecipeDiscounts).ThenInclude(rd => rd.Discount));
            Includes.Add(r => r.Include(r => r.Category));


            if (topRated)
            {
                AddOrderByDesc(r => r.RecipeRatings.Max(rr => rr.Rating));
            }
            else
            {
                AddOrderBy(r => r.RecipeRatings.Min(rr => rr.Rating));
            }
        }
    }

}
