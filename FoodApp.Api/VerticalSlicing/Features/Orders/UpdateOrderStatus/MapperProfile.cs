using AutoMapper;
using FoodApp.Api.VerticalSlicing.Features.Orders.UpdateOrderStatus.Commands;

namespace FoodApp.Api.VerticalSlicing.Features.Orders.UpdateOrderStatus
{
    public class MapperProfile :Profile
    {
        public MapperProfile()
        {
            CreateMap<UpdateOrderStatusRequest, UpdateOrderStatusCommand>();
        }
    }
}
