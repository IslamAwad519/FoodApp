using FoodApp.Api.DTOs;
using System.Security.Claims;

namespace FoodApp.Api.Services
{
    public class UserStateService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserStateService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public UserState GetCurrentUserState()
        {
            var loggedUser = _httpContextAccessor.HttpContext.User;

            if (loggedUser?.Identity?.IsAuthenticated == true)
            {

                var role = loggedUser?.FindFirst("RoleID")?.Value ?? "";
                var id = loggedUser?.FindFirst("ID")?.Value ?? "";
                var name = loggedUser?.FindFirst(ClaimTypes.Name)?.Value ?? "";

                return new UserState
                {
                    ID = id,
                    Name = name,
                    Role = role
                };
            }

            return null;
        }
    }
}
