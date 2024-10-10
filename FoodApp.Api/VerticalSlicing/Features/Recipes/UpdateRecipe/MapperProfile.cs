using AutoMapper;
using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Features.Recipes.UpdateRecipe.Commands;

namespace FoodApp.Api.VerticalSlicing.Features.Recipes.UpdateRecipe
{
    public class MapperProfile :Profile
    {
        public MapperProfile()
        {
            CreateMap<UpdateRecipeCommand, Recipe>()
                 .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
                 .AfterMap(async (src, dest) =>
                 {
                     dest.ImageUrl = await DocumentSettings.UploadFileAsync(src.ImageUrl, "Images");
                 });


            CreateMap<UpdateRecipeRequest, UpdateRecipeCommand>();

        }
    }
}
