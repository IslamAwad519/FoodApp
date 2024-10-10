using AutoMapper;
using FoodApp.Api.VerticalSlicing.Data.Entities;

namespace FoodApp.Api.VerticalSlicing.Features.Discounts.GetActiveDiscounts
{
    public class MapperProfile :Profile
    {
        public MapperProfile()
        {
            CreateMap<GetActiveDiscountsResponse, Discount>().ReverseMap();
        }
    }
}
