using AutoMapper;
using FoodApp.Api.VerticalSlicing.Data.Entities;

namespace FoodApp.Api.VerticalSlicing.Features.Discounts.ViewDiscount
{
    public class MapperProfile :Profile
    {
        public MapperProfile()
        {
            CreateMap<Discount, ViewDiscountResponse>();
        }
    }
}
