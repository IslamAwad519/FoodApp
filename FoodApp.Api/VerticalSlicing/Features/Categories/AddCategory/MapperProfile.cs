using AutoMapper;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Features.Categories.AddCategory.Commands;

namespace FoodApp.Api.VerticalSlicing.Features.Categories.AddCategory
{
    public class MapperProfile :Profile
    {
        public MapperProfile()
        {
            CreateMap<AddCategoryRequest, CreateCategoryCommand>();
            CreateMap<CreateCategoryCommand, Category>();
        }
    }
}
