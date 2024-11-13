namespace FoodApp.Api.VerticalSlicing.Features.Orders.CreateOrder
{
    public class CreateOrderRequest
    {
        public List<OrderItemViewModel> OrderItems { get; set; }
        public AddressViewModel? ShippingAddress { get; set; }
    }
    public class AddressViewModel
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
    }
    public class OrderItemViewModel
    {
        public int RecipeId { get; set; }
        public int Quantity { get; set; }
    }

}
