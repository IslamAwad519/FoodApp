using FoodApp.Api.Abstraction;
using FoodApp.Api.Data.Entities.RecipeEntity;
using FoodApp.Api.DTOs;
using FoodApp.Api.Errors;
using FoodApp.Api.Repository.Specification;
using FoodApp.Api.Response;
using MediatR;
using ProjectManagementSystem.Helper;

namespace FoodApp.Api.CQRS.Recipes.Queries;

public record GetRecipesQuery(SpecParams SpecParams) : IRequest<Result<IEnumerable<RecipeResponse>>>;

public class GetRecipesQueryHandler : BaseRequestHandler<GetRecipesQuery, Result<IEnumerable<RecipeResponse>>>
{
    public GetRecipesQueryHandler(RequestParameters requestParameters) : base(requestParameters) { }

    public override async Task<Result<IEnumerable<RecipeResponse>>> Handle(GetRecipesQuery request, CancellationToken cancellationToken)
    {
        var spec = new RecipeSpec(request.SpecParams);
        var recipe = await _unitOfWork.Repository<Recipe>().GetAllWithSpecAsync(spec);

        if (recipe == null)
        {
            return Result.Failure<IEnumerable<RecipeResponse>>(RecipeErrors.RecipeNotFound);
        }

        var response = recipe.Select(r => r.Map<RecipeResponse>());

        return Result.Success(response);
    }
}