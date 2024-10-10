using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Features.Account;
using FoodApp.Api.VerticalSlicing.Features.Recipes.Common;
using FoodApp.Api.VerticalSlicing.Features.Recipes.ViewRecipe.Queries;
using MediatR;

namespace FoodApp.Api.VerticalSlicing.Features.Recipes.AddRecipeToFavourite.Commands
{
    public record AddRecipeToFavouritesCommand(int RecipeId) : IRequest<Result<bool>>;
    public class AddRecipeToFavouritesCommandHandler : BaseRequestHandler<AddRecipeToFavouritesCommand, Result<bool>>
    {
        public AddRecipeToFavouritesCommandHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public override async Task<Result<bool>> Handle(AddRecipeToFavouritesCommand request, CancellationToken cancellationToken)
        {
            var userId = string.IsNullOrEmpty(_userState.ID) ? 0 : int.Parse(_userState.ID); ;
            if (userId == 0)
            {
                return Result.Failure<bool>(UserErrors.UserNotAuthenticated);
            }

            var recipeResult = await _mediator.Send(new GetRecipeByIdQuery(request.RecipeId), cancellationToken);
            if (!recipeResult.IsSuccess)
            {
                return Result.Failure<bool>(recipeResult.Error);
            }
            var recipe = recipeResult.Data;

            var favouriteRecipeResult = await _mediator.Send(new GetFavouriteRecipeByUserIdAndRecipeIdQuery(userId, request.RecipeId), cancellationToken);

            if (favouriteRecipeResult.IsSuccess)
            {
                return Result.Failure<bool>(RecipeErrors.FavouriteRecipeAlreadyExists);
            }

            var favouriteRecipe = new FavouriteRecipe
            {
                UserId = userId,
                RecipeId = recipe.Id
            };

            await _unitOfWork.Repository<FavouriteRecipe>().AddAsync(favouriteRecipe);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success(true);
        }
    }
}
