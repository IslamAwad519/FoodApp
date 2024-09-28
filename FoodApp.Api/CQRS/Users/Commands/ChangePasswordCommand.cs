using FoodApp.Api.Abstraction;
using FoodApp.Api.CQRS.Users.Queries;
using FoodApp.Api.Data.Entities;
using FoodApp.Api.Errors;
using FoodApp.Api.Repository.Interface;
using MediatR;
using ProjectManagementSystem.Helper;

namespace FoodApp.Api.CQRS.Users.Commands
{
    public record ChangePasswordCommand(
    string Email,
    string CurrentPassword,
    string NewPassword) : IRequest<Result<bool>>;


    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result<bool>>
    {
        private readonly IGenericRepository<User> _userRepo;
        private readonly IMediator _mediator;

        public ChangePasswordCommandHandler(IGenericRepository<User> userRepo, IMediator mediator)
        {
            _userRepo = userRepo;
            _mediator = mediator;
        }
        public async Task<Result<bool>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var userResult = await _mediator.Send(new GetUserByEmailQuery(request.Email));

            if (!userResult.IsSuccess)
            {
                return Result.Failure<bool>(UserErrors.UserNotFound);
            }

            var user = userResult.Data;

            if (!PasswordHasher.checkPassword(request.CurrentPassword, user.PasswordHash))
            {
                return Result.Failure<bool>(UserErrors.InvalidCurrentPassword);
            }

            user.PasswordHash = PasswordHasher.HashPassword(request.NewPassword);
            _userRepo.Update(user);
            await _userRepo.SaveChangesAsync();

            return Result.Success(true);
        }
    
    }
}

