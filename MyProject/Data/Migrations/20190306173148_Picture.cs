using Microsoft.EntityFrameworkCore.Migrations;

namespace MyProject.Migrations
{
    public partial class Picture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_AspNetUsers_ApplicationUserId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_ApplicationUserId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Developer",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Games",
                newName: "Picture");

            migrationBuilder.AlterColumn<string>(
                name: "Picture",
                table: "Games",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeveloperId",
                table: "Games",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_DeveloperId",
                table: "Games",
                column: "DeveloperId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_AspNetUsers_DeveloperId",
                table: "Games",
                column: "DeveloperId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_AspNetUsers_DeveloperId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_DeveloperId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "DeveloperId",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "Picture",
                table: "Games",
                newName: "ApplicationUserId");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Games",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Developer",
                table: "Games",
                maxLength: 60,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_ApplicationUserId",
                table: "Games",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_AspNetUsers_ApplicationUserId",
                table: "Games",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
