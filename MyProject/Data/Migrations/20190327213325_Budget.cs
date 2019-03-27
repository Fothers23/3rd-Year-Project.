using Microsoft.EntityFrameworkCore.Migrations;

namespace MyProject.Migrations
{
    public partial class Budget : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_userId",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Reviews",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_userId",
                table: "Reviews",
                newName: "IX_Reviews_UserId");

            migrationBuilder.RenameColumn(
                name: "Budget",
                table: "AspNetUsers",
                newName: "BudgetTotal");

            migrationBuilder.AddColumn<decimal>(
                name: "Budget",
                table: "Games",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_UserId",
                table: "Reviews",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_UserId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Budget",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Reviews",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                newName: "IX_Reviews_userId");

            migrationBuilder.RenameColumn(
                name: "BudgetTotal",
                table: "AspNetUsers",
                newName: "Budget");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_userId",
                table: "Reviews",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
