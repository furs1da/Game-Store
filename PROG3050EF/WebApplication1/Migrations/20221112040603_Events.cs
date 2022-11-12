using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameStore.Migrations
{
    public partial class Events : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Event",
                columns: new[] { "event_id", "name", "date", "description", "duration"},
                values: new object[,]
                {
                    {  1, "DevGAMM Game Fest 2022", new DateTime(2022, 11, 25,  0, 0, 0, 0, DateTimeKind.Unspecified),  "Join DevGAMM Game Fest 2022, that takes place online on November 23-25 and will be fully dedicated to games and devs.",  "5 hours" },
                    {  2, "Game Wave Festival", new DateTime(2022, 11, 28,  0, 0, 0, 0, DateTimeKind.Unspecified), "The Game Wave Festival is a business, education and entertainment event for the gaming industry professionals.", "2 hours" },
                    {  3, "Africa Games Week 2022", new DateTime(2022, 12, 1,  0, 0, 0, 0, DateTimeKind.Unspecified), "The largest gathering of African game developers, exporters and leaders in the world and this year is pushing the boundaries to make sure the African industry is meeting and building a community to develop the industry.", "3 hours" },
                    {  4, "Reboot Develop Red 2022", new DateTime(2022, 12, 7,  0, 0, 0, 0, DateTimeKind.Unspecified), "Second year of the Canadian edition of world renown Reboot Develop boutique games industry and game developers conference, famous for unbeatable speaker lineups and huge attendance.",  "4 hours" },
                    {  5, "Taipei Game Show 2022", new DateTime(2022, 12, 9,  0, 0, 0, 0, DateTimeKind.Unspecified),  "The 2023 event will run on a hybrid model, encompassing both in-person and online-only events, and the theme this year will be Come in Gameverse",  "7 hours" },
                    {  6, "Reboot Develop Blue 2022", new DateTime(2022, 12, 15,  0, 0, 0, 0, DateTimeKind.Unspecified), "The renowned Croatian developer conference returns to its usual April slot, once again inviting game makers and industry professionals to Dubrovnik. ",  "8 hours" },
                    {  7, "Develop Brighton 2022", new DateTime(2022, 12, 24,  0, 0, 0, 0, DateTimeKind.Unspecified), "Some Description.",  "12 hours" },
                    {  8, "EGX 2023", new DateTime(2022, 12, 28,  0, 0, 0, 0, DateTimeKind.Unspecified),  "The UK’s largest video games event, drawing tens of thousands of visitors to a four-day celebration of all things gaming.",  "2 hours" },
                    {  9, "Awesome Games Done Quick",  new DateTime(2022, 12, 30,  0, 0, 0, 0, DateTimeKind.Unspecified), "Games Done Quick announced that Awesome Games Done Quick 2022, supporting " +
                "Prevent Cancer Foundation, will take place entirely online.", "7 hours" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Event",
                keyColumn: "event_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Event",
                keyColumn: "event_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Event",
                keyColumn: "event_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Event",
                keyColumn: "event_id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Event",
                keyColumn: "event_id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Event",
                keyColumn: "event_id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Event",
                keyColumn: "event_id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Event",
                keyColumn: "event_id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Event",
                keyColumn: "event_id",
                keyValue: 9);
        }
    }
}
