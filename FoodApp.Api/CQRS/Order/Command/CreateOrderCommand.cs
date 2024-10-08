using FoodApp.Api.Abstraction;
using MediatR;

namespace FoodApp.Api.CQRS.Order.Command
{
    public record CreateOrderCommand () : IRequest<Result<OrderToReturnDto>>;

    public class OrderToReturnDto
    {

    }
     


}
