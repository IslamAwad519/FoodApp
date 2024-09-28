using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class updateuserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "OTPExpiration",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OTPExpiration",
                table: "Users");
        }
    }
}
