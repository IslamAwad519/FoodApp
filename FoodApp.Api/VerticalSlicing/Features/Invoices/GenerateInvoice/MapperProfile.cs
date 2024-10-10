using AutoMapper;
using FoodApp.Api.VerticalSlicing.Data.Entities;

namespace FoodApp.Api.VerticalSlicing.Features.Invoices.GenerateInvoice
{
    public class MapperProfile :Profile
    {
        public MapperProfile()
        {
            CreateMap<Invoice, GenerateInvoiceResponse>();
        }
    }
}
