using FoodApp.Api.Abstraction;
using FoodApp.Api.CQRS.Categories.Queries;
using FoodApp.Api.Data.Entities;
using FoodApp.Api.DTOs;
using FoodApp.Api.Errors;
using MediatR;
using ProjectManagementSystem.Helper;

namespace FoodApp.Api.CQRS.Categories.Commands
{
    public record CreateCategoryCommand(string Name) : IRequest<Result<bool>>;
    public class CreateCategoryCommandHandler : BaseRequestHandler<CreateCategoryCommand, Result<bool>>
    {
        public CreateCategoryCommandHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public override async Task<Result<bool>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var existingCategory = await _mediator.Send(new GetCategoryByNameQuery(request.Name), cancellationToken);
            if (existingCategory.IsSuccess)
            {
                return Result.Failure<bool>(CategoryErrors.CategoryAlreadyExists);
            }

            var category = request.Map<Category>();

            await _unitOfWork.Repository<Category>().AddAsync(category);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success(true);
        }
    }
}
