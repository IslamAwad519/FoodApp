﻿namespace FoodApp.Api.VerticalSlicing.Features.Users.ChangeUserPassword
{
    public class ChangePasswordRequest
    {
        public string Email { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}