using Microsoft.EntityFrameworkCore.Migrations;

namespace MyProject.Data.Migrations
{
    public partial class JobSubmissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CompanyDescription",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Developer",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "JobSubmissions",
                columns: table => new
                {
                    Title = table.Column<string>(nullable: false),
                    Developer = table.Column<string>(maxLength: 50, nullable: true),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    AgeRating = table.Column<string>(maxLength: 3, nullable: true),
                    Genre = table.Column<string>(nullable: true),
                    NumberOfPlayers = table.Column<int>(nullable: false),
                    AvailablePlatforms = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSubmissions", x => x.Title);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobSubmissions");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CompanyDescription",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Developer",
                table: "AspNetUsers");
        }
    }
}
