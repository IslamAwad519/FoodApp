using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Features.Account;
using MediatR;

namespace FoodApp.Api.VerticalSlicing.Features.Common
{
    public record GetUserByIdQuery(int UserId) : IRequest<Result<User>>;
    public class GetUserByIdQueryHandler : BaseRequestHandler<GetUserByIdQuery, Result<User>>
    {
        public GetUserByIdQueryHandler(RequestParameters requestParameters) : base(requestParameters)
        {
        }

        public override async Task<Result<User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Repository<User>().FirstAsync(u => u.Id == request.UserId && !u.IsDeleted);

            if (user == null)
            {
                return Result.Failure<User>(UserErrors.UserNotFound);
            }

            return Result.Success(user);
        }
    }

}
