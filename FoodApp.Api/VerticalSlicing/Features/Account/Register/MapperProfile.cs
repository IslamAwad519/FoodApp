using AutoMapper;
using FoodApp.Api.VerticalSlicing.Features.Account.Register.Commands;
using FoodApp.Api.VerticalSlicing.Features.Account.Register.Orchestrator;

namespace FoodApp.Api.VerticalSlicing.Features.Account.Register
{
    public class MapperProfile :Profile
    {
        public MapperProfile()
        {
            CreateMap<RegisterRequest, RegisterCommand>();
            CreateMap<RegisterOrchestrator, RegisterCommand>();
            CreateMap<RegisterRequest, RegisterOrchestrator>();
        }
    }
}
