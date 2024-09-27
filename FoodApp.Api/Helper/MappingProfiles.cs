using AutoMapper;
using FoodApp.Api.CQRS.Users.Commands;
using FoodApp.Api.Data.Entities;
using FoodApp.Api.ViewModels;

namespace FoodApp.Api.Helper
{
    public class MappingProfiles :Profile
    {
        public MappingProfiles()
        {
            CreateMap<RegisterCommand, User>()
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src=>DateTime.Now));

            CreateMap<RegisterViewModel, RegisterCommand>();
        }
    }
}
