using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodApp.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedExpirationDateForResetOtpColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordResetCode",
                table: "Users",
                newName: "PasswordResetOTP");

            migrationBuilder.RenameColumn(
                name: "OTPExpiration",
                table: "Users",
                newName: "VerificationOTPExpiration");

            migrationBuilder.AddColumn<DateTime>(
                name: "PasswordResetOTPExpiration",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordResetOTPExpiration",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "VerificationOTPExpiration",
                table: "Users",
                newName: "OTPExpiration");

            migrationBuilder.RenameColumn(
                name: "PasswordResetOTP",
                table: "Users",
                newName: "PasswordResetCode");
        }
    }
}
