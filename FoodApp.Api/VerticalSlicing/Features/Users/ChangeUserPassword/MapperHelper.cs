using AutoMapper;
using FoodApp.Api.VerticalSlicing.Features.Users.ChangeUserPassword.Commands;

namespace FoodApp.Api.VerticalSlicing.Features.Users.ChangeUserPassword
{
    public class MapperHelper :Profile
    {
        public MapperHelper()
        {

            CreateMap<ChangePasswordRequest, ChangePasswordCommand>();

        }
    }
}
