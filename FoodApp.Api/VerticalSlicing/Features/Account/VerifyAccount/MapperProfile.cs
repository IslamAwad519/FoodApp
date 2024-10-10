using AutoMapper;
using FoodApp.Api.VerticalSlicing.Features.Account.VerifyAccount.Commands;

namespace FoodApp.Api.VerticalSlicing.Features.Account.VerifyAccount
{
    public class MapperProfile :Profile
    {
        public MapperProfile()
        {
            CreateMap<VerifyAccountRequest, VerifyOTPCommand>();
        }
    }
}
