using Microsoft.EntityFrameworkCore.Migrations;

namespace MyProject.Data.Migrations
{
    public partial class Users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Developer",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "RequesterId",
                table: "Games",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CompanyDescription",
                table: "AspNetUsers",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeveloperName",
                table: "AspNetUsers",
                maxLength: 60,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_RequesterId",
                table: "Games",
                column: "RequesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_AspNetUsers_RequesterId",
                table: "Games",
                column: "RequesterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_AspNetUsers_RequesterId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_RequesterId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "RequesterId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "DeveloperName",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyDescription",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Developer",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}
