using FoodApp.Api.VerticalSlicing.Data.Entities;

namespace FoodApp.Api.VerticalSlicing.Data.Repository.Specification.OrderSpec
{
    public class OrderSpecification:BaseSpecification<Order>
    {
       public OrderSpecification(int deliveryManId)
       :base(order => order.DeliveryManId == deliveryManId)
       {
       }
    }
}
