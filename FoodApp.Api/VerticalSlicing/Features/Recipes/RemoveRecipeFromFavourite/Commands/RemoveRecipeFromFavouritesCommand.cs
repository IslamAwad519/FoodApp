using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Features.Account;
using FoodApp.Api.VerticalSlicing.Features.Recipes.Common;
using MediatR;

namespace FoodApp.Api.VerticalSlicing.Features.Recipes.RemoveRecipeFromFavourite.Commands
{
    public record RemoveRecipeFromFavouritesCommand(int RecipeId) : IRequest<Result<bool>>;
    public class RemoveRecipeFromFavouritesCommandHandler : BaseRequestHandler<RemoveRecipeFromFavouritesCommand, Result<bool>>
    {
        public RemoveRecipeFromFavouritesCommandHandler(RequestParameters requestParameters) : base(requestParameters)
        {
        }

        public override async Task<Result<bool>> Handle(RemoveRecipeFromFavouritesCommand request, CancellationToken cancellationToken)
        {
            var userId = string.IsNullOrEmpty(_userState.ID) ? 0 : int.Parse(_userState.ID);
            if (userId == 0)
            {
                return Result.Failure<bool>(UserErrors.UserNotAuthenticated);
            }
            var favouriteRecipeResult = await _mediator.Send(new GetFavouriteRecipeByUserIdAndRecipeIdQuery(userId, request.RecipeId), cancellationToken);

            if (!favouriteRecipeResult.IsSuccess)
            {
                return Result.Failure<bool>(RecipeErrors.FavouriteRecipeNotFound);
            }

            var favouriteRecipe = favouriteRecipeResult.Data;
            _unitOfWork.Repository<FavouriteRecipe>().Delete(favouriteRecipe);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success(true);
        }
    }
}
