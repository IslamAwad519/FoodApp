using FoodApp.Api.Abstraction;
using FoodApp.Api.Data.Entities;
using FoodApp.Api.Repository.Interface;
using MediatR;


namespace ProjectManagementSystem.CQRS.Users.Queries
{
    public record CheckUserExistsQuery(string UserName, string Email) :IRequest<Result<bool>>;

    public class CheckUserExistsQueryHandler : IRequestHandler<CheckUserExistsQuery, Result<bool>>
    {
        private readonly IGenericRepository<User> _genericRepo;

        public CheckUserExistsQueryHandler(IGenericRepository<User> genericRepo)
        {
            _genericRepo = genericRepo;
        }
        public async Task<Result<bool>> Handle(CheckUserExistsQuery request, CancellationToken cancellationToken)
        {
            var existingUser = await _genericRepo
                            .GetAsync(u=>u.Email == request.Email || u.UserName == request.UserName);

            return Result.Success(existingUser.Any());
        }
    }
}
