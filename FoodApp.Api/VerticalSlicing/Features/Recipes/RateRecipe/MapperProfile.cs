using AutoMapper;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Features.Recipes.RateRecipe.Commands;

namespace FoodApp.Api.VerticalSlicing.Features.Recipes.RateRecipe
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<RateRecipeCommand, RecipeRating>()
                .ForMember(dest => dest.Review, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Review)));

            CreateMap<RateRecipeRequest, RateRecipeCommand>();
        }
    }
}
