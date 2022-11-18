using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameStore.Migrations
{
    public partial class deleteGameCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropForeignKey(
                name: "FKGame_Categ26396",
                table: "GameCategories");

            migrationBuilder.DropForeignKey(
                name: "FKGameFeatur387831",
                table: "GameFeatureGames");
    

            migrationBuilder.AddForeignKey(
                name: "FKGame_Categ26396",
                table: "GameCategories",
                column: "Gameid",
                principalTable: "Game",
                principalColumn: "game_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FKGameFeatur387831",
                table: "GameFeatureGames",
                column: "Gameid",
                principalTable: "Game",
                principalColumn: "game_id",
                onDelete: ReferentialAction.Cascade);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropForeignKey(
                name: "FKGame_Categ26396",
                table: "GameCategories");

            migrationBuilder.DropForeignKey(
                name: "FKGameFeatur387831",
                table: "GameFeatureGames");
 

            migrationBuilder.AddForeignKey(
                name: "FKGame_Categ26396",
                table: "GameCategories",
                column: "Gameid",
                principalTable: "Game",
                principalColumn: "game_id");

            migrationBuilder.AddForeignKey(
                name: "FKGameFeatur387831",
                table: "GameFeatureGames",
                column: "Gameid",
                principalTable: "Game",
                principalColumn: "game_id");

        }
    }
}
