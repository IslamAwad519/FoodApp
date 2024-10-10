using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using MediatR;


namespace FoodApp.Api.VerticalSlicing.Features.Account.Register.Queries
{
    public record CheckUserExistsQuery(string UserName, string Email) : IRequest<Result<bool>>;

    public class CheckUserExistsQueryHandler : BaseRequestHandler<CheckUserExistsQuery, Result<bool>>
    {
        public CheckUserExistsQueryHandler(RequestParameters requestParameters) : base(requestParameters) { }
        public override async Task<Result<bool>> Handle(CheckUserExistsQuery request, CancellationToken cancellationToken)
        {
            var existingUser = await _unitOfWork.Repository<User>()
                            .GetAsync(u => u.Email == request.Email || u.UserName == request.UserName);

            return Result.Success(existingUser.Any());
        }
    }
}
