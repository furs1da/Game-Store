using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameStore.Migrations
{
    public partial class WishListMerchandise : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FKWishList705462",
                table: "WishList");

            migrationBuilder.AddForeignKey(
                name: "FK_WishList_Customer_cust_id",
                table: "WishList",
                column: "cust_id",
                principalTable: "Customer",
                principalColumn: "cust_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishList_Customer_cust_id",
                table: "WishList");

            migrationBuilder.AddForeignKey(
                name: "FKWishList705462",
                table: "WishList",
                column: "cust_id",
                principalTable: "Customer",
                principalColumn: "cust_id");
        }
    }
}
