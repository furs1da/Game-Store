using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameStore.Migrations
{
    public partial class GameCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "GameCategories",
                columns: new[] { "Categoryid", "Gameid" },
                values: new object[,]
                {
                    { 7, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 4, 3 },
                    { 1, 4 },
                    { 4, 4 },
                    { 1, 5 },
                    { 4, 5 },
                    { 3, 6 },
                    { 5, 6 },
                    { 4, 7 },
                    { 8, 7 },
                    { 3, 8 },
                    { 5, 8 },
                    { 8, 9 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GameCategories",
                keyColumns: new[] { "Categoryid", "Gameid" },
                keyValues: new object[] { 7, 1 });

            migrationBuilder.DeleteData(
                table: "GameCategories",
                keyColumns: new[] { "Categoryid", "Gameid" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "GameCategories",
                keyColumns: new[] { "Categoryid", "Gameid" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "GameCategories",
                keyColumns: new[] { "Categoryid", "Gameid" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "GameCategories",
                keyColumns: new[] { "Categoryid", "Gameid" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "GameCategories",
                keyColumns: new[] { "Categoryid", "Gameid" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                table: "GameCategories",
                keyColumns: new[] { "Categoryid", "Gameid" },
                keyValues: new object[] { 1, 5 });

            migrationBuilder.DeleteData(
                table: "GameCategories",
                keyColumns: new[] { "Categoryid", "Gameid" },
                keyValues: new object[] { 4, 5 });

            migrationBuilder.DeleteData(
                table: "GameCategories",
                keyColumns: new[] { "Categoryid", "Gameid" },
                keyValues: new object[] { 3, 6 });

            migrationBuilder.DeleteData(
                table: "GameCategories",
                keyColumns: new[] { "Categoryid", "Gameid" },
                keyValues: new object[] { 5, 6 });

            migrationBuilder.DeleteData(
                table: "GameCategories",
                keyColumns: new[] { "Categoryid", "Gameid" },
                keyValues: new object[] { 4, 7 });

            migrationBuilder.DeleteData(
                table: "GameCategories",
                keyColumns: new[] { "Categoryid", "Gameid" },
                keyValues: new object[] { 8, 7 });

            migrationBuilder.DeleteData(
                table: "GameCategories",
                keyColumns: new[] { "Categoryid", "Gameid" },
                keyValues: new object[] { 3, 8 });

            migrationBuilder.DeleteData(
                table: "GameCategories",
                keyColumns: new[] { "Categoryid", "Gameid" },
                keyValues: new object[] { 5, 8 });

            migrationBuilder.DeleteData(
                table: "GameCategories",
                keyColumns: new[] { "Categoryid", "Gameid" },
                keyValues: new object[] { 8, 9 });
        }
    }
}
