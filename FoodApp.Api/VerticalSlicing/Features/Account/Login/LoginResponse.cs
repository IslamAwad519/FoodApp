namespace FoodApp.Api.VerticalSlicing.Features.Account.Login
{
    public class LoginResponse()
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
