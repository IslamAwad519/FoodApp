using AutoMapper;
using FoodApp.Api.VerticalSlicing.Features.Categories.UpdateCategory.Commands;

namespace FoodApp.Api.VerticalSlicing.Features.Categories.UpdateCategory
{
    public class MapperProfile :Profile
    {
        public MapperProfile()
        {
            CreateMap<UpdateCategoryRequest, UpdateCategoryCommand>();
        }
    }
}
