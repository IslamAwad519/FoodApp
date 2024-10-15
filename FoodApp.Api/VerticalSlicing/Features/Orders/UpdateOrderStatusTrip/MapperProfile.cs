using AutoMapper;
using FoodApp.Api.VerticalSlicing.Features.Orders.UpdateOrderStatus.Commands;
using FoodApp.Api.VerticalSlicing.Features.Orders.UpdateOrderStatus;
using FoodApp.Api.VerticalSlicing.Features.Orders.UpdateOrderStatusTrip.Commands;

namespace FoodApp.Api.VerticalSlicing.Features.Orders.UpdateOrderStatusTrip
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<UpdateOrderStatusTripRequest, UpdateOrderStatusTripCommand>();
        }
    }
}
