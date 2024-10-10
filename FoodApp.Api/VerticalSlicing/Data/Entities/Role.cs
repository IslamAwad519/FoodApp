namespace FoodApp.Api.VerticalSlicing.Data.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public bool IsDefault { get; set; } = false;
    }
}
