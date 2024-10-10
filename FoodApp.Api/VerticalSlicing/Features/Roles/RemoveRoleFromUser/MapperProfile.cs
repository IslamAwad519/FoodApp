using AutoMapper;
using FoodApp.Api.VerticalSlicing.Features.Roles.RemoveRoleFromUser.Commands;

namespace FoodApp.Api.VerticalSlicing.Features.Roles.RemoveRoleFromUser
{
    public class MapperProfile :Profile
    {
        public MapperProfile()
        {
            CreateMap<RemoveRoleFromUserRequest, RemoveRoleFromUserCommand> ();
        }
    }
}
