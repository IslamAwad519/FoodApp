using AutoMapper;
using FoodApp.Api.VerticalSlicing.Features.Account.ResendVerificationCode.Commands;

namespace FoodApp.Api.VerticalSlicing.Features.Account.ResendVerificationCode
{
    public class MapperProfile :Profile
    {
        public MapperProfile()
        {
            CreateMap<ResendVerificationCodeRequest, SendVerificationOTP>();
        }
    }
}
