using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameStore.Migrations
{
    public partial class cascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FKCreditCard326673",
                table: "CreditCard");

            migrationBuilder.DropForeignKey(
                name: "FKCustomer_E485607",
                table: "CustomerEvents");

            migrationBuilder.DropForeignKey(
                name: "FKCustomer_E75975",
                table: "CustomerEvents");

            migrationBuilder.DropForeignKey(
                name: "FKFriendsFam820474",
                table: "FriendsFamily");

            migrationBuilder.DropForeignKey(
                name: "FKFriendsFam820475",
                table: "FriendsFamily");

            migrationBuilder.DropForeignKey(
                name: "FKGameImage381528",
                table: "GameImage");

            migrationBuilder.DropForeignKey(
                name: "FKMerchandis483326",
                table: "MerchandiseImage");

            migrationBuilder.DropForeignKey(
                name: "FKOrder547560",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FKOrder609133",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FKOrder861201",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FKReview172984",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FKReview486625",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FKReviewImag856029",
                table: "ReviewImage");

            migrationBuilder.DropForeignKey(
                name: "FK_WishList_Customer_cust_id",
                table: "WishList");

            migrationBuilder.DropForeignKey(
                name: "FK_WishList_Game_game_id",
                table: "WishList");

            migrationBuilder.AddForeignKey(
                name: "FKCreditCard326673",
                table: "CreditCard",
                column: "cust_id",
                principalTable: "Customer",
                principalColumn: "cust_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FKCustomer_E485607",
                table: "CustomerEvents",
                column: "Eventid",
                principalTable: "Event",
                principalColumn: "event_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FKCustomer_E75975",
                table: "CustomerEvents",
                column: "Customerid",
                principalTable: "Customer",
                principalColumn: "cust_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FKGameImage381528",
                table: "GameImage",
                column: "game_id",
                principalTable: "Game",
                principalColumn: "game_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FKMerchandis483326",
                table: "MerchandiseImage",
                column: "merchandise_id",
                principalTable: "Merchandise",
                principalColumn: "merch_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FKOrder547560",
                table: "Order",
                column: "game_id",
                principalTable: "Game",
                principalColumn: "game_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FKOrder609133",
                table: "Order",
                column: "merchandise_id",
                principalTable: "Merchandise",
                principalColumn: "merch_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FKOrder861201",
                table: "Order",
                column: "cust_id",
                principalTable: "Customer",
                principalColumn: "cust_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FKReview172984",
                table: "Review",
                column: "cust_id",
                principalTable: "Customer",
                principalColumn: "cust_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FKReview486625",
                table: "Review",
                column: "game_id",
                principalTable: "Game",
                principalColumn: "game_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FKReviewImag856029",
                table: "ReviewImage",
                column: "review_id",
                principalTable: "Review",
                principalColumn: "review_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WishList_Customer_cust_id",
                table: "WishList",
                column: "cust_id",
                principalTable: "Customer",
                principalColumn: "cust_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WishList_Game_game_id",
                table: "WishList",
                column: "game_id",
                principalTable: "Game",
                principalColumn: "game_id",
                onDelete: ReferentialAction.Cascade);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FKCreditCard326673",
                table: "CreditCard");

            migrationBuilder.DropForeignKey(
                name: "FKCustomer_E485607",
                table: "CustomerEvents");

            migrationBuilder.DropForeignKey(
                name: "FKCustomer_E75975",
                table: "CustomerEvents");

            migrationBuilder.DropForeignKey(
                name: "FKGameImage381528",
                table: "GameImage");

            migrationBuilder.DropForeignKey(
                name: "FKMerchandis483326",
                table: "MerchandiseImage");

            migrationBuilder.DropForeignKey(
                name: "FKOrder547560",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FKOrder609133",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FKOrder861201",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FKReview172984",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FKReview486625",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FKReviewImag856029",
                table: "ReviewImage");

            migrationBuilder.DropForeignKey(
                name: "FK_WishList_Customer_cust_id",
                table: "WishList");

            migrationBuilder.DropForeignKey(
                name: "FK_WishList_Game_game_id",
                table: "WishList");



            migrationBuilder.AddForeignKey(
                name: "FKCreditCard326673",
                table: "CreditCard",
                column: "cust_id",
                principalTable: "Customer",
                principalColumn: "cust_id");

            migrationBuilder.AddForeignKey(
                name: "FKCustomer_E485607",
                table: "CustomerEvents",
                column: "Eventid",
                principalTable: "Event",
                principalColumn: "event_id");

            migrationBuilder.AddForeignKey(
                name: "FKCustomer_E75975",
                table: "CustomerEvents",
                column: "Customerid",
                principalTable: "Customer",
                principalColumn: "cust_id");

            migrationBuilder.AddForeignKey(
                name: "FKGameImage381528",
                table: "GameImage",
                column: "game_id",
                principalTable: "Game",
                principalColumn: "game_id");

            migrationBuilder.AddForeignKey(
                name: "FKMerchandis483326",
                table: "MerchandiseImage",
                column: "merchandise_id",
                principalTable: "Merchandise",
                principalColumn: "merch_id");

            migrationBuilder.AddForeignKey(
                name: "FKOrder547560",
                table: "Order",
                column: "game_id",
                principalTable: "Game",
                principalColumn: "game_id");

            migrationBuilder.AddForeignKey(
                name: "FKOrder609133",
                table: "Order",
                column: "merchandise_id",
                principalTable: "Merchandise",
                principalColumn: "merch_id");

            migrationBuilder.AddForeignKey(
                name: "FKOrder861201",
                table: "Order",
                column: "cust_id",
                principalTable: "Customer",
                principalColumn: "cust_id");

            migrationBuilder.AddForeignKey(
                name: "FKReview172984",
                table: "Review",
                column: "cust_id",
                principalTable: "Customer",
                principalColumn: "cust_id");

            migrationBuilder.AddForeignKey(
                name: "FKReview486625",
                table: "Review",
                column: "game_id",
                principalTable: "Game",
                principalColumn: "game_id");

            migrationBuilder.AddForeignKey(
                name: "FKReviewImag856029",
                table: "ReviewImage",
                column: "review_id",
                principalTable: "Review",
                principalColumn: "review_id");

            migrationBuilder.AddForeignKey(
                name: "FK_WishList_Customer_cust_id",
                table: "WishList",
                column: "cust_id",
                principalTable: "Customer",
                principalColumn: "cust_id");

            migrationBuilder.AddForeignKey(
                name: "FK_WishList_Game_game_id",
                table: "WishList",
                column: "game_id",
                principalTable: "Game",
                principalColumn: "game_id");

        }
    }
}
