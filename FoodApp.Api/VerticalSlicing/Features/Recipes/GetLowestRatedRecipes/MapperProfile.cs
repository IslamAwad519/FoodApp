using AutoMapper;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Features.Recipes.GetTopRatedRecipes;

namespace FoodApp.Api.VerticalSlicing.Features.Recipes.GetLowestRatedRecipes
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Recipe, GetLowestRatedRecipesResponse>()
                  .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.RecipeDiscounts
                   .Where(rd => rd.Discount != null && rd.Discount.IsActive)
                   .Select(rd => rd.Discount.DiscountPercent)
                   .FirstOrDefault()));
        }
    }
}
