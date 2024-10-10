namespace FoodApp.Api.VerticalSlicing.Features.Common;

public record Error(string Description, int? StatusCode)
{
    public static readonly Error None = new(string.Empty, null);
}