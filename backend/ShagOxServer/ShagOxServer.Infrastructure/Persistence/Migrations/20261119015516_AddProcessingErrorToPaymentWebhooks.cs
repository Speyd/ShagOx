using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShagOxServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProcessingErrorToPaymentWebhooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PaymentWebhooks_InvoiceId",
                table: "PaymentWebhooks");

            migrationBuilder.AddColumn<string>(
                name: "ProcessingError",
                table: "PaymentWebhooks",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "PaymentWebhooks",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentWebhooks_InvoiceId",
                table: "PaymentWebhooks",
                column: "InvoiceId",
                unique: true,
                filter: "\"EntityStatus\" IN ('Active', 'Hidden')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PaymentWebhooks_InvoiceId",
                table: "PaymentWebhooks");

            migrationBuilder.DropColumn(
                name: "ProcessingError",
                table: "PaymentWebhooks");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "PaymentWebhooks");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentWebhooks_InvoiceId",
                table: "PaymentWebhooks",
                column: "InvoiceId",
                unique: true);
        }
    }
}
