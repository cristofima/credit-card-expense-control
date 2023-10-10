using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CreditCardExpenseControl.API.Migrations
{
    /// <inheritdoc />
    public partial class NewColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GraceMonths",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "DeferredContributionPercentage",
                table: "CreditCards",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GraceMonths",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "DeferredContributionPercentage",
                table: "CreditCards");
        }
    }
}
