using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Features.Account;
using MediatR;

namespace FoodApp.Api.VerticalSlicing.Features.Common
{
    public record GetUserByEmailQuery(string Email) : IRequest<Result<User>>;

    public class GetUserByEmailQueryHandler : BaseRequestHandler<GetUserByEmailQuery, Result<User>>
    {

        public GetUserByEmailQueryHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public override async Task<Result<User>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Email))
            {
                return Result.Failure<User>(UserErrors.InvalidEmail);
            }

            var user = (await _unitOfWork.Repository<User>().GetAsync(u => u.Email == request.Email)).FirstOrDefault();
            if (user == null)
            {
                return Result.Failure<User>(UserErrors.UserNotFound);
            }

            return Result.Success(user);
        }
    }
}
