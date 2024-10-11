using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Data.Repository.Interface;
using FoodApp.Api.VerticalSlicing.Features.Account;
using MediatR;

namespace FoodApp.Api.VerticalSlicing.Features.Users.GetUserEmailByUserId.Query
{
    public class GetUserEmailByUserIdQuery : IRequest<Result<string>>
    {
        public int UserId { get; set; }

        public GetUserEmailByUserIdQuery(int userId)
        {
            UserId = userId;
        }
    }
    public class GetUserEmailByUserIdQueryHandler : IRequestHandler<GetUserEmailByUserIdQuery, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserEmailByUserIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(GetUserEmailByUserIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Repository<User>().GetByIdAsync(request.UserId);

            if (user == null)
            {
                return Result.Failure<string>(UserErrors.UserNotFound);
            }

            return Result.Success(user.Email);
        }
    }
}
