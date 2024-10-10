using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Common;

using MediatR;

namespace FoodApp.Api.VerticalSlicing.Features.Account.Login.Queries
{
    public record GetUserActiveRefreshTokensQuery(int userId) : IRequest<Result<List<RefreshToken>>>;
    public class GetUserRefreshTokensQueryHandler : BaseRequestHandler<GetUserActiveRefreshTokensQuery, Result<List<RefreshToken>>>
    {

        public GetUserRefreshTokensQueryHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public override async Task<Result<List<RefreshToken>>> Handle(GetUserActiveRefreshTokensQuery request, CancellationToken cancellationToken)
        {
            var refreshTokens = await _unitOfWork.Repository<RefreshToken>()
                    .GetAsync(r => r.UserId == request.userId);

            var activeRefreshTokens = refreshTokens.Where(r => r.IsActive).ToList();

            if (activeRefreshTokens == null || !activeRefreshTokens.Any())
            {
                return Result.Failure<List<RefreshToken>>(UserErrors.NoRefreshTokensFound);
            }

            return Result.Success((List<RefreshToken>)refreshTokens);
        }
    }



}

