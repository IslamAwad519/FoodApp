using FoodApp.Api.VerticalSlicing.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodApp.Api.VerticalSlicing.Data.Repository.Specification.OrderSpec
{
    public class OrderWithUserSpecification :BaseSpecification<Order>
    {
        public OrderWithUserSpecification(int id)
            :base(o=> o.Id == id)
        {
            Includes.Add((o => o.Include(o => o.User)));
        }
    }
}
