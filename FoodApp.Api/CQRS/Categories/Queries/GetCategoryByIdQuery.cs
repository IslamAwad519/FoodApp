using FoodApp.Api.Abstraction;
using FoodApp.Api.Data.Entities;
using FoodApp.Api.DTOs;
using FoodApp.Api.Errors;
using MediatR;

namespace FoodApp.Api.CQRS.Categories.Queries
{
    public record GetCategoryByIdQuery(int CategoryId) : IRequest<Result<Category>>;
    public class GetCategoryByIdQueryHandler : BaseRequestHandler<GetCategoryByIdQuery, Result<Category>>
    {
        public GetCategoryByIdQueryHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public override async Task<Result<Category>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.Repository<Category>().GetByIdAsync(request.CategoryId);
            if (category == null)
            {
                return Result.Failure<Category>(CategoryErrors.CategoryNotFound);
            }

            return Result.Success(category);
        }
    }
}
