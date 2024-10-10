using AutoMapper;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Features.Users.UpdateUserProfile.Commands;

namespace FoodApp.Api.VerticalSlicing.Features.Users.UpdateUserProfile
{
    public class MapperProfile :Profile
    {
        public MapperProfile()
        {
            CreateMap<UpdateUserRequest, UpdateUserProfileCommand>();

            CreateMap<UpdateUserProfileCommand, User>()
                 .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

        }
    }
}
