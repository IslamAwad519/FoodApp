using AutoMapper;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Features.Categories.ViewCategory.Queries;

namespace FoodApp.Api.VerticalSlicing.Features.Categories.ViewCategory
{
    public class MapperProfile :Profile
    {
        public MapperProfile()
        {
            CreateMap<ViewCategoryResponse, Category>().ReverseMap();
            CreateMap<Recipe, RecipesNamesToReturnDto>();

        }
    }
}
