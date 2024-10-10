using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Features.Account.RefreshTokens.Queries;
using MediatR;

namespace FoodApp.Api.VerticalSlicing.Features.Account.RevokeToken.Commands
{
    public record RevokeTokenCommand(string token) : IRequest<Result<bool>>;

    public class RevokeTokenCommandHandler : BaseRequestHandler<RevokeTokenCommand, Result<bool>>
    {
        public RevokeTokenCommandHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public async override Task<Result<bool>> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {
            var userResult = await _mediator.Send(new GetUserByRefreshToken(request.token));

            if (!userResult.IsSuccess)
            {
                return Result.Failure<bool>(UserErrors.InvalidRefreshToken);
            }

            var user = userResult.Data;

            var refreshToken = user.RefreshTokens.FirstOrDefault(rt => rt.Token == request.token);
            if (refreshToken == null || !refreshToken.IsActive)
            {
                return Result.Failure<bool>(UserErrors.InvalidRefreshToken);
            }

            refreshToken.RevokedOn = DateTime.UtcNow;

            var userRepo = _unitOfWork.Repository<User>();

            userRepo.Update(user);
            await userRepo.SaveChangesAsync();

            return Result.Success(true);
        }
    }
}
