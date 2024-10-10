using AutoMapper;
using FoodApp.Api.VerticalSlicing.Features.Account.ResetPassword.Commands;

namespace FoodApp.Api.VerticalSlicing.Features.Account.ResetPassword
{
    public class MapperProfile :Profile
    {
        public MapperProfile()
        {
            CreateMap<ResetPasswordRequest, ResetPasswordCommand>();

        }
    }
}
