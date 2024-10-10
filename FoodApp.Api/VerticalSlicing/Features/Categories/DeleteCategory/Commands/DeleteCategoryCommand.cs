using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Features.Categories.ViewCategory.Queries;
using MediatR;
namespace FoodApp.Api.VerticalSlicing.Features.Categories.DeleteCategory.Commands
{
    public record DeleteCategoryCommand(int CategoryId) : IRequest<Result<bool>>;
    public class DeleteCategoryCommandHandler : BaseRequestHandler<DeleteCategoryCommand, Result<bool>>
    {
        public DeleteCategoryCommandHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public override async Task<Result<bool>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryResult = await _mediator.Send(new GetCategoryByIdQuery(request.CategoryId), cancellationToken);
            if (!categoryResult.IsSuccess)
            {
                return Result.Failure<bool>(CategoryErrors.CategoryNotFound);
            }

            var category = categoryResult.Data;

            _unitOfWork.Repository<Category>().Delete(category);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success(true);
        }
    }
}
