using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameStore.Migrations
{
    public partial class Games : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Game",
                columns: new[] { "game_id", "description", "digital", "discount", "gameStudio", "name", "Pquantity", "price", "releaseDate" },
                values: new object[,]
                {
                    { 1, "The best MOBA game", true, 0, 4, "Dota 2", 10, 0m, new DateTime(2013, 7, 9, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "The best first-person shooter game", true, 0, 4, "CS:GO", 15, 0m, new DateTime(2012, 8, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "The best game", true, 0, 5, "Fortnite", 100, 0m, new DateTime(2017, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "The best battle royal-hero game", true, 0, 2, "Apex Legends", 50, 0m, new DateTime(2021, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Play a new COD game from EA Arts!", true, 0, 2, "Call of Duty®: Modern Warfare® II", 25, 89.99m, new DateTime(2022, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "You are a wanderer with the power to change the fate of The City.", true, 0, 6, "Dying Light 2", 15, 43.99m, new DateTime(2022, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "Better than Fall Guys!", true, 0, 7, "Stumble Guys", 10, 0m, new DateTime(2021, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, "BIGFOOT is a survival horror game about hunting Bigfoot.", true, 0, 8, "BIGFOOT", 24, 26m, new DateTime(2017, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, "EA SPORTS™ FIFA 22 brings the game even closer to the real thing with fundamental gameplay advances and a new season of innovation across every mode.", true, 0, 2, "FIFA 22", 125, 89.99m, new DateTime(2021, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Game",
                keyColumn: "game_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Game",
                keyColumn: "game_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Game",
                keyColumn: "game_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Game",
                keyColumn: "game_id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Game",
                keyColumn: "game_id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Game",
                keyColumn: "game_id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Game",
                keyColumn: "game_id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Game",
                keyColumn: "game_id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Game",
                keyColumn: "game_id",
                keyValue: 9);
        }
    }
}
