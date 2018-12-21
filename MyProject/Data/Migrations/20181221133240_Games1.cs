using Microsoft.EntityFrameworkCore.Migrations;

namespace MyProject.Data.Migrations
{
    public partial class Games1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReviewQuantity",
                table: "Games",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ReviewReward",
                table: "Games",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReviewQuantity",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "ReviewReward",
                table: "Games");
        }
    }
}
