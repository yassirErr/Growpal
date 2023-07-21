using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GrowUp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addCountrymigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contentubes_AspNetUsers_Country_nameId",
                table: "Contentubes");

            migrationBuilder.DropColumn(
                name: "County",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "Country_nameId",
                table: "Contentubes",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "CountyNameId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CountyNameId",
                table: "AspNetUsers",
                column: "CountyNameId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Countries_CountyNameId",
                table: "AspNetUsers",
                column: "CountyNameId",
                principalTable: "Countries",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contentubes_Countries_Country_nameId",
                table: "Contentubes",
                column: "Country_nameId",
                principalTable: "Countries",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Countries_CountyNameId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Contentubes_Countries_Country_nameId",
                table: "Contentubes");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CountyNameId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CountyNameId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Country_nameId",
                table: "Contentubes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "County",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contentubes_AspNetUsers_Country_nameId",
                table: "Contentubes",
                column: "Country_nameId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
