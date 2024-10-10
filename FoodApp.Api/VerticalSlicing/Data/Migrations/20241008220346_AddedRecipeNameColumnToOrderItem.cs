using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddedRecipeNameColumnToOrderItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RecipeName",
                table: "orderItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecipeName",
                table: "orderItems");
        }
    }
}
