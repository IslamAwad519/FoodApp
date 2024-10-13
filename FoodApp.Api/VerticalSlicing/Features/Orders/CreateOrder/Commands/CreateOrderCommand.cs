using FoodApp.Api.Migrations;
using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Common.RabbitMQServices;
using FoodApp.Api.VerticalSlicing.Data.Entities;
using FoodApp.Api.VerticalSlicing.Features.Account;
using FoodApp.Api.VerticalSlicing.Features.Common;
using FoodApp.Api.VerticalSlicing.Features.Orders.CreateOrder.DTOs;
using FoodApp.Api.VerticalSlicing.Features.Orders.CreateOrder.Queries;
using FoodApp.Api.VerticalSlicing.Features.Recipes.ViewRecipe.Queries;
using MediatR;

namespace FoodApp.Api.VerticalSlicing.Features.Orders.CreateOrder.Commands
{
    public record CreateOrderCommand(List<OrderItemDto> OrderItems, AddressDto? ShippingAddress) : IRequest<Result<CreateOrderResponse>>;

    public class CreateOrderCommandHandler : BaseRequestHandler<CreateOrderCommand, Result<CreateOrderResponse>>
    {
        public CreateOrderCommandHandler(RequestParameters requestParameters,RabbitMQPublisherService rabbitMQPublisherService) : base(requestParameters) { }
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
            var existingShippingAddressResult = await _mediator.Send( new GetShippingAddressQuery(int.Parse(userId)));

            if (!existingShippingAddressResult.IsSuccess)
            {
                return Result.Failure<CreateOrderResponse>(OrderErrors.FailedToRetrieveShippingAddress);
            }

            Address? shippingAddress = existingShippingAddressResult.Data;
            if (shippingAddress == null)
            {
                if (request.ShippingAddress == null)
                {
                    return Result.Failure<CreateOrderResponse>(OrderErrors.ShippingAddressRequired);
                }

                var createShippingAddressResult = await _mediator.Send(new CreateShippingAddressCommand(int.Parse(userId), request.ShippingAddress));

                if (!createShippingAddressResult.IsSuccess)
                {
                    return Result.Failure<CreateOrderResponse>(createShippingAddressResult.Error);
                }

                shippingAddress = createShippingAddressResult.Data;
            }

            var order = new Order
            {
                UserId = int.Parse(userId),
                TotalPrice = totalAmount,
                OrderItems = orderItems,
                ShippingAddressId = shippingAddress.Id,
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
                CreatedAt = DateTime.Now,
                OrderItems = orderItems.Select(oi => new OrderItemDetail
                {
                    RecipeId = oi.RecipeId,
                    RecipeName = oi.RecipeName,
                    Quantity = oi.Quantity
                }).ToList()

            };

            _rabbitMQPublisherService.PublishOrderCreatedMessage(orderCreatedMessage);

            return Result.Success(mappedOrder);
        }
    }



}
