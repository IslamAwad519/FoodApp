using FoodApp.Api.Abstraction;
using FoodApp.Api.Data.Entities;
using FoodApp.Api.Errors;
using FoodApp.Api.Repository.Interface;
using MediatR;
using ProjectManagementSystem.CQRS.Users.Queries;
using ProjectManagementSystem.Helper;

namespace FoodApp.Api.CQRS.Users.Commands
{
    public record RegisterCommand(
     string UserName,
     string Email,
     string Country,
     string PhoneNumber,
     string Password) : IRequest<Result>;

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result>
    {
        private readonly IGenericRepository<User> _genericRepo;
        private readonly IMediator _mediator;

        public RegisterCommandHandler(
            IGenericRepository<User> genericRepo,
            IMediator mediator)
        {
            _genericRepo = genericRepo;
            _mediator = mediator;
        }
        public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var userExists = await _mediator.Send(new CheckUserExistsQuery(request.UserName, request.Email));

            if (userExists.Data)
            {
                return Result.Failure<bool>(UserErrors.UserAlreadyExists);
            }

            var user = request.Map<User>();

            user.PasswordHash = PasswordHasher.HashPassword(request.Password);

            await _genericRepo.AddAsync(user);
            await _genericRepo.SaveChangesAsync();

            return Result.Success();

        }
    }
}
