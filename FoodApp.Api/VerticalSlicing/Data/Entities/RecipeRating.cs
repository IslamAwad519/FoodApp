namespace FoodApp.Api.VerticalSlicing.Data.Entities
{
    public class RecipeRating : BaseEntity
    {
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; } = null!;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int Rating { get; set; } 
        public string Review { get; set; } = string.Empty;
        public DateTime RatedOn { get; set; } = DateTime.UtcNow;
    }
}
