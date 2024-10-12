using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Common.RabbitMQServices;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Features.Account;
using FoodApp.Api.VerticalSlicing.Features.Orders.CreateOrder.DTOs;
using FoodApp.Api.VerticalSlicing.Features.Recipes.ViewRecipe.Queries;
using MediatR;

namespace FoodApp.Api.VerticalSlicing.Features.Orders.CreateOrder.Commands
{
    public record CreateOrderCommand(List<OrderItemDto> OrderItems, AddressDto ShippingAddress) : IRequest<Result<CreateOrderResponse>>;

    public class CreateOrderCommandHandler : BaseRequestHandler<CreateOrderCommand, Result<CreateOrderResponse>>
    {
        private readonly RabbitMQPublisherService _rabbitMQPublisherService;

        public CreateOrderCommandHandler(RequestParameters requestParameters,RabbitMQPublisherService rabbitMQPublisherService) : base(requestParameters)
        {
            _rabbitMQPublisherService = rabbitMQPublisherService;
        }
        public override async Task<Result<CreateOrderResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {

            var orderItems = new List<OrderItem>();
            decimal totalAmount = 0;

            foreach (var item in request.OrderItems)
            {
                var recipeResult = await _mediator.Send(new GetRecipeByIdQuery(item.RecipeId));
                var recipe = recipeResult.Data;


                var discount = recipe.RecipeDiscounts
                    .Select(x => x.Discount.DiscountPercent)
                    .FirstOrDefault();

                var discountedPrice = recipe.Price - recipe.Price * (discount / 100);

                var totalAmountForItem = item.Quantity * discountedPrice;
                totalAmount += totalAmountForItem;

                var orderItem = new OrderItem
                {
                    RecipeId = recipe.Id,
                    RecipeName = recipe.Name,
                    Quantity = item.Quantity,
                    Price = recipe.Price
                };

                orderItems.Add(orderItem);
            }
            var userId = _userState.ID;
            if (string.IsNullOrEmpty(userId))
            {
                return Result.Failure<CreateOrderResponse>(UserErrors.UserNotAuthenticated);
            }

            var order = new Order
            {
                UserId = int.Parse(userId),
                TotalPrice = totalAmount,
                OrderItems = orderItems,
                ShppingAddress = request.ShippingAddress.Map<Address>()
            };

            await _unitOfWork.Repository<Order>().AddAsync(order);
            await _unitOfWork.SaveChangesAsync();

            var mappedOrder = order.Map<CreateOrderResponse>();
            var orderCreatedMessage = new OrderCreatedMessage
            {
                OrderId = order.Id,
                UserId = order.UserId,
                UserEmail = "projectsmaster22@gmail.com",
                TotalPrice = order.TotalPrice,
                CreatedAt = DateTime.Now
            };

            _rabbitMQPublisherService.PublishOrderCreatedMessage(orderCreatedMessage);

            return Result.Success(mappedOrder);
        }
    }



}
