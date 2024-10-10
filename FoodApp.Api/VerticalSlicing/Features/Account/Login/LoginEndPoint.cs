using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Features.Account.Login.Commands;
using Microsoft.AspNetCore.Mvc;

namespace FoodApp.Api.VerticalSlicing.Features.Account.Login
{
    //public class LoginEndPoint : BaseController
    //{
    //    public LoginEndPoint(ControllerParameters controllerParameters) : base(controllerParameters) { }

    //    [HttpPost("Login")]
    //    public async Task<Result<LoginResponse>> Login(LoginRequest request)
    //    {
    //        var command = request.Map<LoginCommand>();
    //        var result = await _mediator.Send(command);
    //        if (result.Data == null || string.IsNullOrEmpty(result.Data.RefreshToken))
    //        {
    //            return Result.Failure<LoginResponse>(UserErrors.InvalidCredentials);
    //        }
    //        CookieHelper.SetRefreshTokenCookie(Response, result.Data.RefreshToken);
    //        return result;

    //    }

    //}
}
