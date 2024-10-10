namespace FoodApp.Api.VerticalSlicing.Features.Users.UpdateUserProfile
{
    public class UpdateUserRequest
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Country { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
