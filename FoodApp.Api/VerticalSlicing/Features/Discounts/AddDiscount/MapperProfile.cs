using AutoMapper;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Features.Discounts.AddDiscount.Commands;

namespace FoodApp.Api.VerticalSlicing.Features.Discounts.AddDiscount
{
    public class MapperProfile :Profile 
    {
        public MapperProfile()
        {
            CreateMap<AddDiscountCommand, Discount>();
            CreateMap<AddDiscountRequest, AddDiscountCommand>();

        }
    }
}
