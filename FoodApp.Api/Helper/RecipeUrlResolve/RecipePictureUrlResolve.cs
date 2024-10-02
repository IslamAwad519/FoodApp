using AutoMapper;
using FoodApp.Api.CQRS.Recipes.Commands;
using FoodApp.Api.Data.Entities;

namespace FoodApp.Api.Helper.RecipeUrlResolve;

public class RecipePictureUrlResolve : IValueResolver<CreateRecipeCommand, Recipe, string>
{

    private readonly IConfiguration _configuration;

    public RecipePictureUrlResolve(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string Resolve(CreateRecipeCommand source, Recipe destination, string destMember, ResolutionContext context)
    {
        throw new NotImplementedException();
    }
}

//public class RoomPictureUrlResolve : IValueResolver<RoomToReturnDto, RoomViewModel, List<string>>
//{

//    private readonly IConfiguration _configuration;

//    public RoomPictureUrlResolve(IConfiguration configuration)
//    {
//        _configuration = configuration;
//    }

//    public List<string> Resolve(RoomToReturnDto source, RoomViewModel destination, List<string> destMember, ResolutionContext context)
//    {
//        var images = new List<string>();
//        foreach (var img in source.Images)
//        {
//            if (!string.IsNullOrEmpty(img))
//            {
//                images.Add($"{_configuration["ApiBaseUrl"]}Files/Images/{img}");
//            }
//        }
//        return images;
//    }
//}