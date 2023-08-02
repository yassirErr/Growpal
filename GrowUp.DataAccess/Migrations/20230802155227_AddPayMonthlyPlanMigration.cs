using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GrowUp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddPayMonthlyPlanMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PayMonthlyPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PriceMonthly = table.Column<double>(type: "float", nullable: false),
                    Information = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactMethode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayMonthlyPlans", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PayMonthlyPlans");
        }
    }
}
