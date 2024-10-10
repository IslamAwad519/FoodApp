using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Data.Repository.Specification.RecipeSpec;
using FoodApp.Api.VerticalSlicing.Features.Account;
using MediatR;

namespace FoodApp.Api.VerticalSlicing.Features.Recipes.ViewFavouriteRecipes.Queries
{
    public record GetFavouriteRecipesForLoggedUserQuery() : IRequest<Result<List<ViewFavouriteRecipesResponse>>>;


    public class GetFavouriteRecipesForLoggedUserQueryHandler : BaseRequestHandler<GetFavouriteRecipesForLoggedUserQuery, Result<List<ViewFavouriteRecipesResponse>>>
    {
        public GetFavouriteRecipesForLoggedUserQueryHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public override async Task<Result<List<ViewFavouriteRecipesResponse>>> Handle(GetFavouriteRecipesForLoggedUserQuery request, CancellationToken cancellationToken)
        {
            var userId = string.IsNullOrEmpty(_userState.ID) ? 0 : int.Parse(_userState.ID);

            if (userId == 0)
            {
                return Result.Failure<List<ViewFavouriteRecipesResponse>>(UserErrors.UserNotAuthenticated);
            }

            var spec = new FavouriteRecipesWithSpecification(userId);
            var favouriteRecipes = await _unitOfWork.Repository<FavouriteRecipe>().ListAsync(spec);

            if (favouriteRecipes == null || !favouriteRecipes.Any())
            {
                return Result.Failure<List<ViewFavouriteRecipesResponse>>(RecipeErrors.FavouriteRecipeNotFound);
            }

            var favouriteRecipesDto = favouriteRecipes
                .Select(fr => new ViewFavouriteRecipesResponse(fr.Recipe.Id, fr.Recipe.Name))
                .ToList();

            return Result.Success(favouriteRecipesDto);
        }
    }
}
