using AutoMapper;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Features.Discounts.UpdateDiscount.Commands;

namespace FoodApp.Api.VerticalSlicing.Features.Discounts.UpdateDiscount
{
    public class MapperProfile :Profile
    {
        public MapperProfile()
        {
            CreateMap<UpdateDiscountCommand, Discount>()
                .ForMember(dest => dest.DiscountPercent, opt => opt.Condition(src => src.DiscountPercent.HasValue))
                .ForMember(dest => dest.StartDate, opt => opt.Condition(src => src.StartDate.HasValue))
                .ForMember(dest => dest.EndDate, opt => opt.Condition(src => src.EndDate.HasValue));

        }
    }
}
