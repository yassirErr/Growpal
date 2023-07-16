using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GrowUp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addWatchtubeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Watchtubes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContentId = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VideoLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReactubeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Watchtubes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Watchtubes_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Watchtubes_Contentubes_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Contentubes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Watchtubes_Reactubes_ReactubeId",
                        column: x => x.ReactubeId,
                        principalTable: "Reactubes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Watchtubes_ApplicationUserId",
                table: "Watchtubes",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Watchtubes_ContentId",
                table: "Watchtubes",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_Watchtubes_ReactubeId",
                table: "Watchtubes",
                column: "ReactubeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Watchtubes");
        }
    }
}
