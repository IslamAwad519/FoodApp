using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddDeliveryManAndAddressTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShppingAddress_City",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "ShppingAddress_Country",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "ShppingAddress_FirstName",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "ShppingAddress_LastName",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "ShppingAddress_Street",
                table: "orders");

            migrationBuilder.AddColumn<int>(
                name: "DeliveryManId",
                table: "orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShippingAddressId",
                table: "orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StatusTrip",
                table: "orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_orders_ShippingAddressId",
                table: "orders",
                column: "ShippingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_UserId",
                table: "Address",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_Address_ShippingAddressId",
                table: "orders",
                column: "ShippingAddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_DeliveryMan_DeliveryManId",
                table: "orders",
                column: "DeliveryManId",
                principalTable: "DeliveryMan",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_Address_ShippingAddressId",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_DeliveryMan_DeliveryManId",
                table: "orders");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "DeliveryMan");

            migrationBuilder.DropIndex(
                name: "IX_orders_DeliveryManId",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_ShippingAddressId",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "DeliveryManId",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "ShippingAddressId",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "StatusTrip",
                table: "orders");

            migrationBuilder.AddColumn<string>(
                name: "ShppingAddress_City",
                table: "orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShppingAddress_Country",
                table: "orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShppingAddress_FirstName",
                table: "orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShppingAddress_LastName",
                table: "orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShppingAddress_Street",
                table: "orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
