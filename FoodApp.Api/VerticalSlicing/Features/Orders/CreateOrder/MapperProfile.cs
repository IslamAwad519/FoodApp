using AutoMapper;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Features.Orders.CreateOrder.Commands;
using FoodApp.Api.VerticalSlicing.Features.Orders.CreateOrder.DTOs;

namespace FoodApp.Api.VerticalSlicing.Features.Orders.CreateOrder
{
    public class MapperProfile :Profile
    {
        public MapperProfile()
        {
            CreateMap<Order, CreateOrderResponse>();
            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<AddressDto, Address>();
            CreateMap<CreateOrderRequest, CreateOrderCommand>();

            CreateMap<CreateOrderRequest, CreateOrderCommand>();

            CreateMap<OrderItemViewModel, OrderItemDto>();

            CreateMap<AddressViewModel, AddressDto>();
        }
    }
}
