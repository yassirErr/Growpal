using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GrowUp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addApplicationUserIdIntoReactubemigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Reactubes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Reactubes_ApplicationUserId",
                table: "Reactubes",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reactubes_AspNetUsers_ApplicationUserId",
                table: "Reactubes",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reactubes_AspNetUsers_ApplicationUserId",
                table: "Reactubes");

            migrationBuilder.DropIndex(
                name: "IX_Reactubes_ApplicationUserId",
                table: "Reactubes");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Reactubes");
        }
    }
}
