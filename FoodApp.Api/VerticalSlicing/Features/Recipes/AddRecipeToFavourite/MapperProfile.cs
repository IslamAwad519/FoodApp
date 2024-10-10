using AutoMapper;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Features.Recipes.AddRecipeToFavourite.Commands;

namespace FoodApp.Api.VerticalSlicing.Features.Recipes.AddRecipeToFavourite
{
    public class MapperProfile :Profile
    {
        public MapperProfile()
        {
            CreateMap<AddRecipeToFavoriteRequest, AddRecipeToFavouritesCommand>();
            CreateMap<AddRecipeToFavouritesCommand, FavouriteRecipe>().ReverseMap();
        }
    }
}
