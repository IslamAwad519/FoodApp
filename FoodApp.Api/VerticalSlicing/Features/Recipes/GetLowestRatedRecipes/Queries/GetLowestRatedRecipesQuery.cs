using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Data.Repository.Specification.RecipeSpec;
using FoodApp.Api.VerticalSlicing.Features.Recipes.GetTopRatedRecipes;
using MediatR;

namespace FoodApp.Api.VerticalSlicing.Features.Recipes.GetLowestRatedRecipes.Queries
{
    public record GetLowestRatedRecipesQuery(int NumberOfRecipes) : IRequest<Result<IEnumerable<GetLowestRatedRecipesResponse>>>;

    public class GetLowestRatedRecipesQueryHandler : BaseRequestHandler<GetLowestRatedRecipesQuery, Result<IEnumerable<GetLowestRatedRecipesResponse>>>
    {
        public GetLowestRatedRecipesQueryHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public async override Task<Result<IEnumerable<GetLowestRatedRecipesResponse>>> Handle(GetLowestRatedRecipesQuery request, CancellationToken cancellationToken)
        {

            var spec = new RecipeRatingSpecification(false);
            var recipes = await _unitOfWork.Repository<Recipe>().GetAllWithSpecAsync(spec);

            var topRatedRecipes = recipes
                    .Take(request.NumberOfRecipes);


            var mappedRecipes = topRatedRecipes.Map<IEnumerable<GetLowestRatedRecipesResponse>>();

            return Result.Success(mappedRecipes);
        }
    }
}
