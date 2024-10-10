using AutoMapper;
using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Features.Recipes.AddRecipe.Commands;

namespace FoodApp.Api.VerticalSlicing.Features.Recipes.AddRecipe
{
    public class MapperProfile :Profile
    {
        public MapperProfile()
        {
            CreateMap<CreateRecipeCommand, Recipe>()
              .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
              .AfterMap(async (src, dest) =>
              {
                  dest.ImageUrl = await DocumentSettings.UploadFileAsync(src.ImageUrl, "Images");
              });

            CreateMap<CreateRecipeRequest, CreateRecipeCommand>();

        }
    }
}
