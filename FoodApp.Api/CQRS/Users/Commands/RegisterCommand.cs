using FoodApp.Api.Abstraction;
using FoodApp.Api.Data.Entities;
using FoodApp.Api.DTOs;
using FoodApp.Api.Errors;
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

    public class RegisterCommandHandler : BaseRequestHandler<RegisterCommand, Result>
    {
        public RegisterCommandHandler(RequestParameters requestParameters) : base(requestParameters) { }
        public override async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var userExists = await _mediator.Send(new CheckUserExistsQuery(request.UserName, request.Email));

            if (userExists.Data)
            {
                return Result.Failure<bool>(UserErrors.UserAlreadyExists);
            }

            var user = request.Map<User>();

            user.PasswordHash = PasswordHasher.HashPassword(request.Password);

            var userRepo = _unitOfWork.Repository<User>();

            await userRepo.AddAsync(user);
            await userRepo.SaveChangesAsync();

            return Result.Success();

        }
    }
}
