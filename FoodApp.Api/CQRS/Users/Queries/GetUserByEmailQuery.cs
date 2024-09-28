using FoodApp.Api.Abstraction;
using FoodApp.Api.Data.Entities;
using FoodApp.Api.Errors;
using FoodApp.Api.Repository.Interface;
using MediatR;

namespace FoodApp.Api.CQRS.Users.Queries
{
    public record GetUserByEmailQuery(string Email) : IRequest<Result<User>>;

    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, Result<User>>
    {
         
        private readonly IGenericRepository<User> _userRepository;

        public GetUserByEmailQueryHandler(IGenericRepository<User> userRepository)
        {     
            _userRepository = userRepository;
        }

        public async Task<Result<User>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Email))
            {
                return Result.Failure<User>(UserErrors.InvalidEmail);
            }

            var user = (await _userRepository.GetAsync(u => u.Email == request.Email)).FirstOrDefault();
            if (user == null)
            {
                return Result.Failure<User>(UserErrors.UserNotFound);
            }

            return Result.Success(user);
        }
    }
}
