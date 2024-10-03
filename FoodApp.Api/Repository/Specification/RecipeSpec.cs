using FoodApp.Api.Data.Entities.RecipeEntity;
using Microsoft.EntityFrameworkCore;

namespace FoodApp.Api.Repository.Specification;

public class RecipeSpec : BaseSpecification<Recipe>
{
    public RecipeSpec(SpecParams spec)
    {
        Includes.Add(p => p.Include(p => p.Category));

        if (!string.IsNullOrEmpty(spec.Search))
        {
            Criteria = p => p.Name.ToLower().Contains(spec.Search.ToLower());
        }

        if (!string.IsNullOrEmpty(spec.Sort))
        {
            switch (spec.Sort.ToLower())
            {
                case "Name":
                    AddOrderBy(p => p.Name);
                    break;
                default:
                    AddOrderBy(p => p.Price);
                    break;
            }
        }

        ApplyPagination(spec.PageSize * (spec.PageIndex - 1), spec.PageSize);
    }
}