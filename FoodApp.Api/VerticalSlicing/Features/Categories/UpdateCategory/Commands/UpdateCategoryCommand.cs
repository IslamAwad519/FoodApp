using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Features.Categories.ViewCategory.Queries;
using MediatR;

namespace FoodApp.Api.VerticalSlicing.Features.Categories.UpdateCategory.Commands
{
    public record UpdateCategoryCommand(int CategoryId, string Name) : IRequest<Result<bool>>;
    public class UpdateCategoryCommandHandler : BaseRequestHandler<UpdateCategoryCommand, Result<bool>>
    {
        public UpdateCategoryCommandHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public override async Task<Result<bool>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryResult = await _mediator.Send(new GetCategoryByIdQuery(request.CategoryId), cancellationToken);
            if (!categoryResult.IsSuccess)
            {
                return Result.Failure<bool>(CategoryErrors.CategoryNotFound);
            }

            var category = categoryResult.Data;
            category.Name = request.Name;

            _unitOfWork.Repository<Category>().Update(category);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success(true);
        }
    }
}
