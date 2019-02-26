using Microsoft.EntityFrameworkCore.Migrations;

namespace MyProject.Migrations
{
    public partial class Cascadedeletereviews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Games_MyGameGameID",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "MyGameGameID",
                table: "Reviews",
                newName: "GameID");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_MyGameGameID",
                table: "Reviews",
                newName: "IX_Reviews_GameID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Games_GameID",
                table: "Reviews",
                column: "GameID",
                principalTable: "Games",
                principalColumn: "GameID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Games_GameID",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "GameID",
                table: "Reviews",
                newName: "MyGameGameID");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_GameID",
                table: "Reviews",
                newName: "IX_Reviews_MyGameGameID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Games_MyGameGameID",
                table: "Reviews",
                column: "MyGameGameID",
                principalTable: "Games",
                principalColumn: "GameID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
