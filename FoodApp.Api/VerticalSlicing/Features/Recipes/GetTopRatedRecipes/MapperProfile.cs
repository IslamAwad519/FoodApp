using AutoMapper;
using FoodApp.Api.VerticalSlicing.Data.Entities;

namespace FoodApp.Api.VerticalSlicing.Features.Recipes.GetTopRatedRecipes
{
    public class MapperProfile :Profile
    {
        public MapperProfile()
        {
            CreateMap<Recipe, GetTopRatedRecipesResponse>()
                  .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.RecipeDiscounts
                   .Where(rd => rd.Discount != null && rd.Discount.IsActive)
                   .Select(rd => rd.Discount.DiscountPercent)
                   .FirstOrDefault()));
        }
    }
}
