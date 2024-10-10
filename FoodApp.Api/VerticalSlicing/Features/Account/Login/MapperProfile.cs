using AutoMapper;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Features.Account.Login.Commands;
using FoodApp.Api.VerticalSlicing.Features.Account.VerifyAccount;
using FoodApp.Api.VerticalSlicing.Features.Account.VerifyAccount.Commands;

namespace FoodApp.Api.VerticalSlicing.Features.Account.Login
{
    public class MapperProfile :Profile
    {
        public MapperProfile()
        {
            CreateMap<LoginRequest, LoginCommand>();

            //CreateMap<User, LoginResponse>()
            //   .ForMember(dest => dest.RefreshToken, opt => opt.MapFrom(src =>
            //       src.RefreshTokens
            //          .Where(r => r.IsActive)
            //          .Select(r => r.Token)
            //          .FirstOrDefault()));
        }
    }
}
