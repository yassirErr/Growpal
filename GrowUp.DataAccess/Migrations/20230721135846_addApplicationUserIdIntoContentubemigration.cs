using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GrowUp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addApplicationUserIdIntoContentubemigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Contentubes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Contentubes_ApplicationUserId",
                table: "Contentubes",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contentubes_AspNetUsers_ApplicationUserId",
                table: "Contentubes",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contentubes_AspNetUsers_ApplicationUserId",
                table: "Contentubes");

            migrationBuilder.DropIndex(
                name: "IX_Contentubes_ApplicationUserId",
                table: "Contentubes");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Contentubes");
        }
    }
}
