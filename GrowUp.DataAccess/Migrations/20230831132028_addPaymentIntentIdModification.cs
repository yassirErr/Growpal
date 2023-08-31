using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GrowUp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addPaymentIntentIdModification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymenyIntentId",
                table: "OrderHeaders",
                newName: "PaymentItentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentItentId",
                table: "OrderHeaders",
                newName: "PaymenyIntentId");
        }
    }
}
