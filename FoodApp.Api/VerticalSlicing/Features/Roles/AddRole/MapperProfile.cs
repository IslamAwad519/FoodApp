using AutoMapper;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Features.Roles.AddRole.Commands;

namespace FoodApp.Api.VerticalSlicing.Features.Roles.AddRole
{
    public class MapperProfile :Profile
    {
        public MapperProfile()
        {
            CreateMap<AddRoleRequest, CreateRoleCommand>();

            CreateMap<CreateRoleCommand, Role>()
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.roleName));

        }
    }
}
