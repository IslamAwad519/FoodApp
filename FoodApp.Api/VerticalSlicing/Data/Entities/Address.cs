namespace FoodApp.Api.VerticalSlicing.Data.Entities
{
    public class Address :BaseEntity
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public int UserId { get; set; } 
        public User User { get; set; }

    }
}