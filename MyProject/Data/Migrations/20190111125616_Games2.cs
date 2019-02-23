using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyProject.Data.Migrations
{
    public partial class Games2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GId",
                table: "Games",
                newName: "GameID");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Games",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DatePosted",
                table: "Games",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "GameLink",
                table: "Games",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GraphicQuality = table.Column<int>(nullable: false),
                    Playability = table.Column<int>(nullable: false),
                    StoryCharacterDevelopment = table.Column<int>(nullable: false),
                    GameplayControls = table.Column<int>(nullable: false),
                    Multiplayer = table.Column<int>(nullable: false),
                    Pros = table.Column<string>(nullable: true),
                    Cons = table.Column<string>(nullable: true),
                    WrittenReview = table.Column<string>(nullable: true),
                    Summary = table.Column<string>(maxLength: 200, nullable: true),
                    DatePosted = table.Column<DateTime>(nullable: false),
                    MyGameGameID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewID);
                    table.ForeignKey(
                        name: "FK_Reviews_Games_MyGameGameID",
                        column: x => x.MyGameGameID,
                        principalTable: "Games",
                        principalColumn: "GameID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_MyGameGameID",
                table: "Reviews",
                column: "MyGameGameID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropColumn(
                name: "DatePosted",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "GameLink",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "GameID",
                table: "Games",
                newName: "GId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Games",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 250,
                oldNullable: true);
        }
    }
}
