using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Data.Repository.Interface;
using FoodApp.Api.VerticalSlicing.Data.Repository.Specification.RecipeSpec;
using MediatR;

namespace FoodApp.Api.VerticalSlicing.Features.Recipes.GetTopRatedRecipes.Queries
{
    public record GetTopRatedRecipesQuery(int NumberOfRecipes) : IRequest<Result<IEnumerable<GetTopRatedRecipesResponse>>>;

    public class GetTopRatedRecipesQueryHandler : BaseRequestHandler<GetTopRatedRecipesQuery, Result<IEnumerable<GetTopRatedRecipesResponse>>>
    {
        public GetTopRatedRecipesQueryHandler(RequestParameters requestParameters) :base(requestParameters) { }

        public async override Task<Result<IEnumerable<GetTopRatedRecipesResponse>>> Handle(GetTopRatedRecipesQuery request, CancellationToken cancellationToken)
        {

            var spec = new RecipeRatingSpecification(true);
            var recipes = await _unitOfWork.Repository<Recipe>().GetAllWithSpecAsync(spec);

            var topRatedRecipes = recipes
                    .Take(request.NumberOfRecipes);
     

            var mappedRecipes = topRatedRecipes.Map<IEnumerable<GetTopRatedRecipesResponse>>();

            return Result.Success(mappedRecipes);
        }
    }

}
