using AutoMapper;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Features.Recipes.Common.Helper;

namespace FoodApp.Api.VerticalSlicing.Features.Recipes.ListRecipes
{
    public class MapperProfile :Profile
    {
        public MapperProfile()
        {
            CreateMap<Recipe, ListRecipesResponse>()
                   .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom<RecipePictureUrlResolve>())
                   .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                   .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.RecipeDiscounts
                   .Where(rd => rd.Discount != null && rd.Discount.IsActive)
                   .Select(rd => rd.Discount.DiscountPercent)
                   .FirstOrDefault())).ReverseMap();
        }
    }
}
