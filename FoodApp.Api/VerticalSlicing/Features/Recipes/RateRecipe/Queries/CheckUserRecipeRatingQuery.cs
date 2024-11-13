using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Data.Repository.Interface;
using MediatR;

namespace FoodApp.Api.VerticalSlicing.Features.Recipes.RateRecipe.Queries
{
    public record CheckUserRecipeRatingQuery(int UserId, int RecipeId) : IRequest<bool>;

    public class CheckUserRecipeRatingQueryHandler : BaseRequestHandler<CheckUserRecipeRatingQuery, bool>
    {

        public CheckUserRecipeRatingQueryHandler(RequestParameters requestParameters) :base(requestParameters) { }

        public async override Task<bool> Handle(CheckUserRecipeRatingQuery request, CancellationToken cancellationToken)
        {
  
            var existingRating = await _unitOfWork.Repository<RecipeRating>()
                .GetAsync(rr => rr.UserId == request.UserId && rr.RecipeId == request.RecipeId);

            return existingRating.Any();
        }
    }

}
