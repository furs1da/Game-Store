using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameStore.Migrations
{
    public partial class GameFeature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "GameFeature",
                columns: new[] { "feature_id", "feature" },
                values: new object[,]
                {
                    { 1, "Free-to-play" },
                    { 2, "Multiplayer" },
                    { 3, "Casual" },
                    { 4, "Party-based" },
                    { 5, "Anime" },
                    { 6, "Co-Operative" },
                    { 7, "LAN" },
                    { 8, "Singleplayer" },
                    { 9, "Sports" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GameFeature",
                keyColumn: "feature_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "GameFeature",
                keyColumn: "feature_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "GameFeature",
                keyColumn: "feature_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "GameFeature",
                keyColumn: "feature_id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "GameFeature",
                keyColumn: "feature_id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "GameFeature",
                keyColumn: "feature_id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "GameFeature",
                keyColumn: "feature_id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "GameFeature",
                keyColumn: "feature_id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "GameFeature",
                keyColumn: "feature_id",
                keyValue: 9);
        }
    }
}
