using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameStore.Migrations
{
    public partial class Wishlist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FKWishList795792",
                table: "WishList");

            migrationBuilder.DropForeignKey(
                name: "FKWishList952486",
                table: "WishList");

            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "event_id",
                keyValue: 7,
                column: "description",
                value: "Some Description.");

            migrationBuilder.AddForeignKey(
                name: "FK_WishList_Game_game_id",
                table: "WishList",
                column: "game_id",
                principalTable: "Game",
                principalColumn: "game_id");

            migrationBuilder.AddForeignKey(
                name: "FK_WishList_Merchandise_merchandise_id",
                table: "WishList",
                column: "merchandise_id",
                principalTable: "Merchandise",
                principalColumn: "merch_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishList_Game_game_id",
                table: "WishList");

            migrationBuilder.DropForeignKey(
                name: "FK_WishList_Merchandise_merchandise_id",
                table: "WishList");

            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "event_id",
                keyValue: 7,
                column: "description",
                value: "Develop Brighton is the only event in the UK that brings the entire game-making industry together - from global superstars to micro indies, to learn from each other, share ideas and experiences, network and do business in a friendly and inclusive environment. ");

            migrationBuilder.AddForeignKey(
                name: "FKWishList795792",
                table: "WishList",
                column: "merchandise_id",
                principalTable: "Merchandise",
                principalColumn: "merch_id");

            migrationBuilder.AddForeignKey(
                name: "FKWishList952486",
                table: "WishList",
                column: "game_id",
                principalTable: "Game",
                principalColumn: "game_id");
        }
    }
}
