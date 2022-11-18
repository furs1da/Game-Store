using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameStore.Migrations
{
    public partial class GamePlatformCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FKPlatform_G103358",
                table: "PlatformGames");

            migrationBuilder.AddForeignKey(
                name: "FKPlatform_G103358",
                table: "PlatformGames",
                column: "Gameid",
                principalTable: "Game",
                principalColumn: "game_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FKPlatform_G103358",
                table: "PlatformGames");

            migrationBuilder.AddForeignKey(
                name: "FKPlatform_G103358",
                table: "PlatformGames",
                column: "Gameid",
                principalTable: "Game",
                principalColumn: "game_id");
        }
    }
}
