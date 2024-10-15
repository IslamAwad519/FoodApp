using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class updatedeliveryManAndUserRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "DeliveryMan",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryMan_UserId",
                table: "DeliveryMan",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryMan_Users_UserId",
                table: "DeliveryMan",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryMan_Users_UserId",
                table: "DeliveryMan");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryMan_UserId",
                table: "DeliveryMan");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "DeliveryMan");
        }
    }
}
