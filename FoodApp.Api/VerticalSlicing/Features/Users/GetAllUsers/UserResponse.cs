namespace FoodApp.Api.VerticalSlicing.Features.Users.GetAllUsers
{

    public class UserResponse
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
