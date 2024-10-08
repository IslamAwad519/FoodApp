using FoodApp.Api.DTOs;

namespace FoodApp.Api.ViewModels
{
    public class CreateOrderViewModel
    {
        public List<OrderItemViewModel> OrderItems { get; set; }
        public AddressViewModel ShippingAddress { get; set; }
    }

}
