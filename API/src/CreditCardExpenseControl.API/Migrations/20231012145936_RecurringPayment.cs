using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CreditCardExpenseControl.API.Migrations
{
    /// <inheritdoc />
    public partial class RecurringPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRecurringPayment",
                table: "Transactions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "RecurringPaymentEndDate",
                table: "Transactions",
                type: "date",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRecurringPayment",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "RecurringPaymentEndDate",
                table: "Transactions");
        }
    }
}
