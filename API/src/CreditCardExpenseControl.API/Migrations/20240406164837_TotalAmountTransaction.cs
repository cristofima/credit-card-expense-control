using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CreditCardExpenseControl.API.Migrations
{
    /// <inheritdoc />
    public partial class TotalAmountTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmountTransaction",
                table: "Transactions",
                type: "decimal(7,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalAmountTransaction",
                table: "Transactions");
        }
    }
}
