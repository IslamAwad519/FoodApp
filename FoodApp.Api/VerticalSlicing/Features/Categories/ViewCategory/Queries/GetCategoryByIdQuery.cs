using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Data.Repository.Specification.CategorySpec;
using MediatR;

namespace FoodApp.Api.VerticalSlicing.Features.Categories.ViewCategory.Queries
{
    public record GetCategoryByIdQuery(int CategoryId) : IRequest<Result<Category>>;

    public record RecipesNamesToReturnDto(int Id, string Name);

    public class GetCategoryByIdQueryHandler : BaseRequestHandler<GetCategoryByIdQuery, Result<Category>>
    {
        public GetCategoryByIdQueryHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public override async Task<Result<Category>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var spec = new CategoryWithRecipesSpecification(request.CategoryId);
            var category = await _unitOfWork.Repository<Category>().GetByIdWithSpecAsync(spec);
            if (category == null)
            {
                return Result.Failure<Category>(CategoryErrors.CategoryNotFound);
            }


            return Result.Success(category);
        }
    }
}
