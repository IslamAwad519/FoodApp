﻿namespace FoodApp.Api.Data.Entities.RecipeEntity;

public class Category : BaseEntity
{
    public string Name { get; set; } = null!;
    public ICollection<Recipe> Recipes { get; set; } = new HashSet<Recipe>();
}