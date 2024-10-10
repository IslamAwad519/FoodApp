using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Data.Repository.Specification;
using Microsoft.EntityFrameworkCore;

namespace FoodApp.Api.VerticalSlicing.Data.Repository.Specification.CategorySpec
{
    public class CategoryWithRecipesSpecification : BaseSpecification<Category>
    {
        public CategoryWithRecipesSpecification(int id)
            : base(c => c.Id == id)
        {
            Includes.Add(c => c.Include(c => c.Recipes));
        }
    }
}
