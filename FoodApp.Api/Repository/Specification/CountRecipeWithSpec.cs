using FoodApp.Api.Data.Entities.RecipeEntity;

namespace FoodApp.Api.Repository.Specification;

public class CountRecipeWithSpec : BaseSpecification<Recipe>
{
    public CountRecipeWithSpec(SpecParams specParams)
        : base(p => !p.IsDeleted)
    {

    }
}