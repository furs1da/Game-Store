using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameStore.Migrations
{
    public partial class PlatformGame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PlatformGames",
                columns: new[] { "Gameid", "Platformid" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 4, 1 },
                    { 5, 1 },
                    { 6, 1 },
                    { 7, 1 },
                    { 8, 1 },
                    { 9, 1 },
                    { 3, 2 },
                    { 4, 2 },
                    { 5, 2 },
                    { 6, 2 },
                    { 9, 2 },
                    { 3, 3 },
                    { 4, 3 },
                    { 5, 3 },
                    { 6, 3 },
                    { 9, 3 },
                    { 7, 6 },
                    { 1, 7 },
                    { 2, 7 },
                    { 3, 7 },
                    { 4, 7 },
                    { 5, 7 },
                    { 6, 7 },
                    { 8, 7 },
                    { 9, 7 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlatformGames",
                keyColumns: new[] { "Gameid", "Platformid" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "PlatformGames",
                keyColumns: new[] { "Gameid", "Platformid" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "PlatformGames",
                keyColumns: new[] { "Gameid", "Platformid" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "PlatformGames",
                keyColumns: new[] { "Gameid", "Platformid" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "PlatformGames",
                keyColumns: new[] { "Gameid", "Platformid" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "PlatformGames",
                keyColumns: new[] { "Gameid", "Platformid" },
                keyValues: new object[] { 6, 1 });

            migrationBuilder.DeleteData(
                table: "PlatformGames",
                keyColumns: new[] { "Gameid", "Platformid" },
                keyValues: new object[] { 7, 1 });

            migrationBuilder.DeleteData(
                table: "PlatformGames",
                keyColumns: new[] { "Gameid", "Platformid" },
                keyValues: new object[] { 8, 1 });

            migrationBuilder.DeleteData(
                table: "PlatformGames",
                keyColumns: new[] { "Gameid", "Platformid" },
                keyValues: new object[] { 9, 1 });

            migrationBuilder.DeleteData(
                table: "PlatformGames",
                keyColumns: new[] { "Gameid", "Platformid" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "PlatformGames",
                keyColumns: new[] { "Gameid", "Platformid" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "PlatformGames",
                keyColumns: new[] { "Gameid", "Platformid" },
                keyValues: new object[] { 5, 2 });

            migrationBuilder.DeleteData(
                table: "PlatformGames",
                keyColumns: new[] { "Gameid", "Platformid" },
                keyValues: new object[] { 6, 2 });

            migrationBuilder.DeleteData(
                table: "PlatformGames",
                keyColumns: new[] { "Gameid", "Platformid" },
                keyValues: new object[] { 9, 2 });

            migrationBuilder.DeleteData(
                table: "PlatformGames",
                keyColumns: new[] { "Gameid", "Platformid" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "PlatformGames",
                keyColumns: new[] { "Gameid", "Platformid" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "PlatformGames",
                keyColumns: new[] { "Gameid", "Platformid" },
                keyValues: new object[] { 5, 3 });

            migrationBuilder.DeleteData(
                table: "PlatformGames",
                keyColumns: new[] { "Gameid", "Platformid" },
                keyValues: new object[] { 6, 3 });

            migrationBuilder.DeleteData(
                table: "PlatformGames",
                keyColumns: new[] { "Gameid", "Platformid" },
                keyValues: new object[] { 9, 3 });

            migrationBuilder.DeleteData(
                table: "PlatformGames",
                keyColumns: new[] { "Gameid", "Platformid" },
                keyValues: new object[] { 7, 6 });

            migrationBuilder.DeleteData(
                table: "PlatformGames",
                keyColumns: new[] { "Gameid", "Platformid" },
                keyValues: new object[] { 1, 7 });

            migrationBuilder.DeleteData(
                table: "PlatformGames",
                keyColumns: new[] { "Gameid", "Platformid" },
                keyValues: new object[] { 2, 7 });

            migrationBuilder.DeleteData(
                table: "PlatformGames",
                keyColumns: new[] { "Gameid", "Platformid" },
                keyValues: new object[] { 3, 7 });

            migrationBuilder.DeleteData(
                table: "PlatformGames",
                keyColumns: new[] { "Gameid", "Platformid" },
                keyValues: new object[] { 4, 7 });

            migrationBuilder.DeleteData(
                table: "PlatformGames",
                keyColumns: new[] { "Gameid", "Platformid" },
                keyValues: new object[] { 5, 7 });

            migrationBuilder.DeleteData(
                table: "PlatformGames",
                keyColumns: new[] { "Gameid", "Platformid" },
                keyValues: new object[] { 6, 7 });

            migrationBuilder.DeleteData(
                table: "PlatformGames",
                keyColumns: new[] { "Gameid", "Platformid" },
                keyValues: new object[] { 8, 7 });

            migrationBuilder.DeleteData(
                table: "PlatformGames",
                keyColumns: new[] { "Gameid", "Platformid" },
                keyValues: new object[] { 9, 7 });
        }
    }
}
