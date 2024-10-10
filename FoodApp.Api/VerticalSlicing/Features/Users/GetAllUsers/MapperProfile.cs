using AutoMapper;
using FoodApp.Api.VerticalSlicing.Data.Entities;

namespace FoodApp.Api.VerticalSlicing.Features.Users.GetAllUsers
{
    public class MapperProfile :Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserResponse>();
        }
    }
}
