using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameStore.Migrations
{
    public partial class Event : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "event_id",
                keyValue: 7,
                columns: new[] { "description", "name" },
                values: new object[] { "Develop Brighton is the only event in the UK that brings the entire game-making industry together - from global superstars to micro indies, to learn from each other, share ideas and experiences, network and do business in a friendly and inclusive environment. ", "Develop Brighton 2022" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Event",
                keyColumn: "event_id",
                keyValue: 7,
                columns: new[] { "description", "name" },
                values: new object[] { "Develop:Brighton is the only event in the UK that brings the entire game-making industry together - from global superstars to micro indies, to learn from each other, share ideas and experiences, network and do business in a friendly and inclusive environment. ", "Develop:Brighton 2022" });
        }
    }
}
