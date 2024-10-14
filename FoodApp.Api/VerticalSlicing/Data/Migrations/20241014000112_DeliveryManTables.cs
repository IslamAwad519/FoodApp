using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class DeliveryManTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeliveryManId",
                table: "orders",
                type: "int",
                nullable: true,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "StatusTrip",
                table: "orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DeliveryMan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryMan", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_orders_DeliveryManId",
                table: "orders",
                column: "DeliveryManId");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_DeliveryMan_DeliveryManId",
                table: "orders",
                column: "DeliveryManId",
                principalTable: "DeliveryMan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_DeliveryMan_DeliveryManId",
                table: "orders");

            migrationBuilder.DropTable(
                name: "DeliveryMan");

            migrationBuilder.DropIndex(
                name: "IX_orders_DeliveryManId",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "DeliveryManId",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "StatusTrip",
                table: "orders");
        }
    }
}
