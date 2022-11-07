using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameStore.Migrations
{
    public partial class Addresses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryGame");

            migrationBuilder.DropTable(
                name: "GameGameFeature");

            migrationBuilder.DropTable(
                name: "GamePlatform");

            migrationBuilder.DropColumn(
                name: "mailingAddress",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "mailingPostalCode",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "shippingAddress",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "shippingPostalCode",
                table: "Customer");

            migrationBuilder.AddColumn<int>(
                name: "mailingAddress_id",
                table: "Customer",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "shippingAddress_id",
                table: "Customer",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MailingAddress",
                columns: table => new
                {
                    mailingAddressId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    City = table.Column<string>(type: "varchar(55)", unicode: false, maxLength: 55, nullable: false),
                    Province = table.Column<int>(type: "int", nullable: false),
                    PostalCode = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailingAddress", x => x.mailingAddressId);
                });

            migrationBuilder.CreateTable(
                name: "ShippingAddress",
                columns: table => new
                {
                    shippingAddressId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    City = table.Column<string>(type: "varchar(55)", unicode: false, maxLength: 55, nullable: false),
                    Province = table.Column<int>(type: "int", nullable: false),
                    PostalCode = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingAddress", x => x.shippingAddressId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_mailingAddress_id",
                table: "Customer",
                column: "mailingAddress_id");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_shippingAddress_id",
                table: "Customer",
                column: "shippingAddress_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_MailingAddress",
                table: "Customer",
                column: "mailingAddress_id",
                principalTable: "MailingAddress",
                principalColumn: "mailingAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_ShippingAddress",
                table: "Customer",
                column: "shippingAddress_id",
                principalTable: "ShippingAddress",
                principalColumn: "shippingAddressId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_MailingAddress",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_ShippingAddress",
                table: "Customer");

            migrationBuilder.DropTable(
                name: "MailingAddress");

            migrationBuilder.DropTable(
                name: "ShippingAddress");

            migrationBuilder.DropIndex(
                name: "IX_Customer_mailingAddress_id",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_shippingAddress_id",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "mailingAddress_id",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "shippingAddress_id",
                table: "Customer");

            migrationBuilder.AddColumn<string>(
                name: "mailingAddress",
                table: "Customer",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "mailingPostalCode",
                table: "Customer",
                type: "varchar(16)",
                unicode: false,
                maxLength: 16,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "shippingAddress",
                table: "Customer",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "shippingPostalCode",
                table: "Customer",
                type: "varchar(16)",
                unicode: false,
                maxLength: 16,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CategoryGame",
                columns: table => new
                {
                    Categoryid = table.Column<int>(type: "int", nullable: false),
                    Gameid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryGame", x => new { x.Categoryid, x.Gameid });
                });

            migrationBuilder.CreateTable(
                name: "GameGameFeature",
                columns: table => new
                {
                    GameFeatureid = table.Column<int>(type: "int", nullable: false),
                    Gameid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameGameFeature", x => new { x.GameFeatureid, x.Gameid });
                });

            migrationBuilder.CreateTable(
                name: "GamePlatform",
                columns: table => new
                {
                    Gameid = table.Column<int>(type: "int", nullable: false),
                    Platformid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePlatform", x => new { x.Gameid, x.Platformid });
                });
        }
    }
}
