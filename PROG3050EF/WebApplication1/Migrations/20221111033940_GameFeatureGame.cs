using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameStore.Migrations
{
    public partial class GameFeatureGame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "GameFeatureGames",
                columns: new[] { "GameFeatureid", "Gameid" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 1, 4 },
                    { 1, 7 },
                    { 2, 1 },
                    { 2, 2 },
                    { 2, 3 },
                    { 2, 4 },
                    { 2, 5 },
                    { 2, 7 },
                    { 2, 9 },
                    { 6, 5 },
                    { 6, 6 },
                    { 6, 8 },
                    { 7, 9 },
                    { 8, 5 },
                    { 8, 6 },
                    { 8, 8 },
                    { 8, 9 },
                    { 9, 9 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GameFeatureGames",
                keyColumns: new[] { "GameFeatureid", "Gameid" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "GameFeatureGames",
                keyColumns: new[] { "GameFeatureid", "Gameid" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "GameFeatureGames",
                keyColumns: new[] { "GameFeatureid", "Gameid" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "GameFeatureGames",
                keyColumns: new[] { "GameFeatureid", "Gameid" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "GameFeatureGames",
                keyColumns: new[] { "GameFeatureid", "Gameid" },
                keyValues: new object[] { 1, 7 });

            migrationBuilder.DeleteData(
                table: "GameFeatureGames",
                keyColumns: new[] { "GameFeatureid", "Gameid" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "GameFeatureGames",
                keyColumns: new[] { "GameFeatureid", "Gameid" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "GameFeatureGames",
                keyColumns: new[] { "GameFeatureid", "Gameid" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "GameFeatureGames",
                keyColumns: new[] { "GameFeatureid", "Gameid" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "GameFeatureGames",
                keyColumns: new[] { "GameFeatureid", "Gameid" },
                keyValues: new object[] { 2, 5 });

            migrationBuilder.DeleteData(
                table: "GameFeatureGames",
                keyColumns: new[] { "GameFeatureid", "Gameid" },
                keyValues: new object[] { 2, 7 });

            migrationBuilder.DeleteData(
                table: "GameFeatureGames",
                keyColumns: new[] { "GameFeatureid", "Gameid" },
                keyValues: new object[] { 2, 9 });

            migrationBuilder.DeleteData(
                table: "GameFeatureGames",
                keyColumns: new[] { "GameFeatureid", "Gameid" },
                keyValues: new object[] { 6, 5 });

            migrationBuilder.DeleteData(
                table: "GameFeatureGames",
                keyColumns: new[] { "GameFeatureid", "Gameid" },
                keyValues: new object[] { 6, 6 });

            migrationBuilder.DeleteData(
                table: "GameFeatureGames",
                keyColumns: new[] { "GameFeatureid", "Gameid" },
                keyValues: new object[] { 6, 8 });

            migrationBuilder.DeleteData(
                table: "GameFeatureGames",
                keyColumns: new[] { "GameFeatureid", "Gameid" },
                keyValues: new object[] { 7, 9 });

            migrationBuilder.DeleteData(
                table: "GameFeatureGames",
                keyColumns: new[] { "GameFeatureid", "Gameid" },
                keyValues: new object[] { 8, 5 });

            migrationBuilder.DeleteData(
                table: "GameFeatureGames",
                keyColumns: new[] { "GameFeatureid", "Gameid" },
                keyValues: new object[] { 8, 6 });

            migrationBuilder.DeleteData(
                table: "GameFeatureGames",
                keyColumns: new[] { "GameFeatureid", "Gameid" },
                keyValues: new object[] { 8, 8 });

            migrationBuilder.DeleteData(
                table: "GameFeatureGames",
                keyColumns: new[] { "GameFeatureid", "Gameid" },
                keyValues: new object[] { 8, 9 });

            migrationBuilder.DeleteData(
                table: "GameFeatureGames",
                keyColumns: new[] { "GameFeatureid", "Gameid" },
                keyValues: new object[] { 9, 9 });
        }
    }
}
