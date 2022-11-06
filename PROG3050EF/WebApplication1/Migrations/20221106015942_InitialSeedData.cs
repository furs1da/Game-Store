using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameStore.Migrations
{
    public partial class InitialSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "category_id", "name" },
                values: new object[,]
                {
                    { 1, "Shooter" },
                    { 2, "Fighting" },
                    { 3, "Stealth" },
                    { 4, "RPG" },
                    { 5, "Survival" },
                    { 6, "Platformer" },
                    { 7, "MOBA" },
                    { 8, "Simulator" },
                    { 9, "Puzzler" }
                });

            migrationBuilder.InsertData(
                table: "Platform",
                columns: new[] { "platform_id", "name" },
                values: new object[,]
                {
                    { 1, "PC" },
                    { 2, "Xbox" },
                    { 3, "PlayStation" },
                    { 4, "Nintendo" },
                    { 5, "VR" },
                    { 6, "Mobile" },
                    { 7, "Steam OS" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "category_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "category_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "category_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "category_id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "category_id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "category_id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "category_id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "category_id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "category_id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Platform",
                keyColumn: "platform_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Platform",
                keyColumn: "platform_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Platform",
                keyColumn: "platform_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Platform",
                keyColumn: "platform_id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Platform",
                keyColumn: "platform_id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Platform",
                keyColumn: "platform_id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Platform",
                keyColumn: "platform_id",
                keyValue: 7);
        }
    }
}
