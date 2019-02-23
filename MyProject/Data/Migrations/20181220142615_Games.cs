using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyProject.Data.Migrations
{
    public partial class Games : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobSubmissions");

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    GId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 60, nullable: false),
                    Developer = table.Column<string>(maxLength: 60, nullable: true),
                    Description = table.Column<string>(maxLength: 150, nullable: true),
                    AgeRating = table.Column<string>(maxLength: 3, nullable: true),
                    Genre = table.Column<string>(maxLength: 60, nullable: true),
                    NumberOfPlayers = table.Column<int>(nullable: false),
                    AvailablePlatforms = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.GId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.CreateTable(
                name: "JobSubmissions",
                columns: table => new
                {
                    Title = table.Column<string>(nullable: false),
                    AgeRating = table.Column<string>(maxLength: 3, nullable: true),
                    AvailablePlatforms = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    Developer = table.Column<string>(maxLength: 50, nullable: true),
                    Genre = table.Column<string>(nullable: true),
                    NumberOfPlayers = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSubmissions", x => x.Title);
                });
        }
    }
}
