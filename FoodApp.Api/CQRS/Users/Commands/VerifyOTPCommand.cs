using FoodApp.Api.Abstraction;
using FoodApp.Api.Data.Entities;
using FoodApp.Api.Errors;
using FoodApp.Api.Repository.Interface;
using MediatR;

namespace FoodApp.Api.CQRS.Users.Commands
{
    public record VerifyOTPCommand(string Email, string OTP) : IRequest<Result<bool>>;

    public class VerifyOTPCommandHandler : IRequestHandler<VerifyOTPCommand, Result<bool>>
    {
        IGenericRepository<User> _userRepository;
        private readonly IMediator _mediator;

        public VerifyOTPCommandHandler(IGenericRepository<User> userRepository,IMediator mediator)
        {
            _userRepository = userRepository;
            _mediator = mediator;
        }

        public async Task<Result<bool>> Handle(VerifyOTPCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FirstAsync(u => u.Email == request.Email);

            if (user is null || (user.OTPExpiration is not null && user.OTPExpiration < DateTime.Now) || (user.VerificationOTP is not null && user.VerificationOTP != request.OTP))
            {
                return Result.Failure<bool>(UserErrors.UserNotFound);
            }

            user.VerificationOTP = null;
            user.OTPExpiration = null;
            user.IsEmailVerified = true;

            await _userRepository.SaveChangesAsync();

            return Result.Success(true);
        }
    }
}
