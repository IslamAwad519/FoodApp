using AutoMapper;
using FoodApp.Api.VerticalSlicing.Features.Orders.UpdateOrderStatus.Commands;
using FoodApp.Api.VerticalSlicing.Features.Orders.UpdateOrderStatus;
using FoodApp.Api.VerticalSlicing.Features.Orders.AssignOrdersToDeliveryMan.Commands;

namespace FoodApp.Api.VerticalSlicing.Features.Orders.AssignOrdersToDeliveryMan
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<AssignOrdersToDeliveryManRequest, AssignOrdersToDeliveryManCommand>();
        }
    }
}
