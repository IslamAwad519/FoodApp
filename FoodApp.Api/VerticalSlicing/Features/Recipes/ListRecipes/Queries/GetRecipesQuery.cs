using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Data.Repository.Specification;
using FoodApp.Api.VerticalSlicing.Data.Repository.Specification.RecipeSpec;
using FoodApp.Api.VerticalSlicing.Features.Recipes;
using MediatR;

namespace FoodApp.Api.VerticalSlicing.Features.Recipes.ListRecipes.Queries;

public record GetRecipesQuery(SpecParams SpecParams) : IRequest<Result<IEnumerable<ListRecipesResponse>>>;

public class GetRecipesQueryHandler : BaseRequestHandler<GetRecipesQuery, Result<IEnumerable<ListRecipesResponse>>>
{
    public GetRecipesQueryHandler(RequestParameters requestParameters) : base(requestParameters) { }

    public override async Task<Result<IEnumerable<ListRecipesResponse>>> Handle(GetRecipesQuery request, CancellationToken cancellationToken)
    {
        var spec = new RecipeSpecification(request.SpecParams);
        var recipe = await _unitOfWork.Repository<Recipe>().GetAllWithSpecAsync(spec);


        if (recipe == null)
        {
            return Result.Failure<IEnumerable<ListRecipesResponse>>(RecipeErrors.RecipeNotFound);
        }

        var response = recipe.Map<IEnumerable<ListRecipesResponse>>();

        return Result.Success(response);
    }
}