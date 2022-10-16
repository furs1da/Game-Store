using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameStore.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    category_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.category_id);
                });

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
                name: "Employee",
                columns: table => new
                {
                    emp_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Employee__1299A861977CC9A6", x => x.emp_id);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    event_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    date = table.Column<DateTime>(type: "datetime", nullable: true),
                    description = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    duration = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.event_id);
                });

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    game_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    price = table.Column<decimal>(type: "decimal(19,2)", nullable: true),
                    Pquantity = table.Column<int>(type: "int", nullable: true),
                    description = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    discount = table.Column<int>(type: "int", nullable: true),
                    releaseDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    gameStudio = table.Column<int>(type: "int", nullable: true),
                    digital = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.game_id);
                });

            migrationBuilder.CreateTable(
                name: "GameFeature",
                columns: table => new
                {
                    feature_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    feature = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__GameFeat__7906CBD7BF1F29A9", x => x.feature_id);
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

            migrationBuilder.CreateTable(
                name: "Merchandise",
                columns: table => new
                {
                    merch_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    description = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    game_id = table.Column<int>(type: "int", nullable: true),
                    price = table.Column<decimal>(type: "decimal(19,2)", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Merchand__1937E9C0E88FCC6F", x => x.merch_id);
                });

            migrationBuilder.CreateTable(
                name: "Platform",
                columns: table => new
                {
                    platform_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platform", x => x.platform_id);
                });

            migrationBuilder.CreateTable(
                name: "Game_Category",
                columns: table => new
                {
                    Gameid = table.Column<int>(type: "int", nullable: false),
                    Categoryid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Game_Cat__0B57EBB7A12BD34A", x => new { x.Gameid, x.Categoryid });
                    table.ForeignKey(
                        name: "FKGame_Categ26396",
                        column: x => x.Gameid,
                        principalTable: "Game",
                        principalColumn: "game_id");
                    table.ForeignKey(
                        name: "FKGame_Categ786403",
                        column: x => x.Categoryid,
                        principalTable: "Category",
                        principalColumn: "category_id");
                });

            migrationBuilder.CreateTable(
                name: "GameImage",
                columns: table => new
                {
                    gameImg_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    game_id = table.Column<int>(type: "int", nullable: false),
                    path = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    extention = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    isCoverImage = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__GameImag__132575EA33968AE7", x => x.gameImg_id);
                    table.ForeignKey(
                        name: "FKGameImage381528",
                        column: x => x.game_id,
                        principalTable: "Game",
                        principalColumn: "game_id");
                });

            migrationBuilder.CreateTable(
                name: "GameFeature_Game",
                columns: table => new
                {
                    GameFeatureid = table.Column<int>(type: "int", nullable: false),
                    Gameid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__GameFeat__0E2BBB9711BBC777", x => new { x.GameFeatureid, x.Gameid });
                    table.ForeignKey(
                        name: "FKGameFeatur387831",
                        column: x => x.Gameid,
                        principalTable: "Game",
                        principalColumn: "game_id");
                    table.ForeignKey(
                        name: "FKGameFeatur415415",
                        column: x => x.GameFeatureid,
                        principalTable: "GameFeature",
                        principalColumn: "feature_id");
                });

            migrationBuilder.CreateTable(
                name: "MerchandiseImage",
                columns: table => new
                {
                    merchImg_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    merchandise_id = table.Column<int>(type: "int", nullable: false),
                    path = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    extention = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Merchand__4D99D94A9EAE7002", x => x.merchImg_id);
                    table.ForeignKey(
                        name: "FKMerchandis483326",
                        column: x => x.merchandise_id,
                        principalTable: "Merchandise",
                        principalColumn: "merch_id");
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    cust_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nickname = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    firstName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    lastName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    gender = table.Column<int>(type: "int", nullable: true),
                    dob = table.Column<DateTime>(type: "datetime", nullable: true),
                    recievePromotion = table.Column<bool>(type: "bit", nullable: true),
                    shippingAddress = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    shippingPostalCode = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: true),
                    mailingAddress = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    mailingPostalCode = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: true),
                    preferedPlatform_id = table.Column<int>(type: "int", nullable: false),
                    preferedCategory_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Customer__A1B71F90C6489007", x => x.cust_id);
                    table.ForeignKey(
                        name: "FKCustomer55737",
                        column: x => x.preferedPlatform_id,
                        principalTable: "Platform",
                        principalColumn: "platform_id");
                    table.ForeignKey(
                        name: "FKCustomer578024",
                        column: x => x.preferedCategory_id,
                        principalTable: "Category",
                        principalColumn: "category_id");
                });

            migrationBuilder.CreateTable(
                name: "Platform_Game",
                columns: table => new
                {
                    Platformid = table.Column<int>(type: "int", nullable: false),
                    Gameid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Platform__B7F882CFAE338AEA", x => new { x.Platformid, x.Gameid });
                    table.ForeignKey(
                        name: "FKPlatform_G103358",
                        column: x => x.Gameid,
                        principalTable: "Game",
                        principalColumn: "game_id");
                    table.ForeignKey(
                        name: "FKPlatform_G477683",
                        column: x => x.Platformid,
                        principalTable: "Platform",
                        principalColumn: "platform_id");
                });

            migrationBuilder.CreateTable(
                name: "CreditCard",
                columns: table => new
                {
                    credit_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cust_id = table.Column<int>(type: "int", nullable: false),
                    cardNumber = table.Column<int>(type: "int", nullable: false),
                    cardName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    expirationDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CreditCa__C15A9C36A58DE279", x => x.credit_id);
                    table.ForeignKey(
                        name: "FKCreditCard326673",
                        column: x => x.cust_id,
                        principalTable: "Customer",
                        principalColumn: "cust_id");
                });

            migrationBuilder.CreateTable(
                name: "Customer_Event",
                columns: table => new
                {
                    Customerid = table.Column<int>(type: "int", nullable: false),
                    Eventid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Customer__333907D60F8269FF", x => new { x.Customerid, x.Eventid });
                    table.ForeignKey(
                        name: "FKCustomer_E485607",
                        column: x => x.Eventid,
                        principalTable: "Event",
                        principalColumn: "event_id");
                    table.ForeignKey(
                        name: "FKCustomer_E75975",
                        column: x => x.Customerid,
                        principalTable: "Customer",
                        principalColumn: "cust_id");
                });

            migrationBuilder.CreateTable(
                name: "FriendsFamily",
                columns: table => new
                {
                    friend_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cust_id1 = table.Column<int>(type: "int", nullable: false),
                    cust_id2 = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    dateConnected = table.Column<DateTime>(type: "datetime", nullable: false),
                    isFamily = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__FriendsF__3FA1E155F831F53C", x => x.friend_id);
                    table.ForeignKey(
                        name: "FKFriendsFam820474",
                        column: x => x.cust_id1,
                        principalTable: "Customer",
                        principalColumn: "cust_id");
                    table.ForeignKey(
                        name: "FKFriendsFam820475",
                        column: x => x.cust_id2,
                        principalTable: "Customer",
                        principalColumn: "cust_id");
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    order_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order_no = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    cust_id = table.Column<int>(type: "int", nullable: false),
                    game_id = table.Column<int>(type: "int", nullable: false),
                    merchandise_id = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "datetime", nullable: false),
                    isShipped = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.order_id);
                    table.ForeignKey(
                        name: "FKOrder547560",
                        column: x => x.game_id,
                        principalTable: "Game",
                        principalColumn: "game_id");
                    table.ForeignKey(
                        name: "FKOrder609133",
                        column: x => x.merchandise_id,
                        principalTable: "Merchandise",
                        principalColumn: "merch_id");
                    table.ForeignKey(
                        name: "FKOrder861201",
                        column: x => x.cust_id,
                        principalTable: "Customer",
                        principalColumn: "cust_id");
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    review_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    game_id = table.Column<int>(type: "int", nullable: false),
                    cust_id = table.Column<int>(type: "int", nullable: false),
                    rate = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    date = table.Column<DateTime>(type: "datetime", nullable: true),
                    title = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.review_id);
                    table.ForeignKey(
                        name: "FKReview172984",
                        column: x => x.cust_id,
                        principalTable: "Customer",
                        principalColumn: "cust_id");
                    table.ForeignKey(
                        name: "FKReview486625",
                        column: x => x.game_id,
                        principalTable: "Game",
                        principalColumn: "game_id");
                });

            migrationBuilder.CreateTable(
                name: "WishList",
                columns: table => new
                {
                    wish_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cust_id = table.Column<int>(type: "int", nullable: false),
                    game_id = table.Column<int>(type: "int", nullable: false),
                    merchandise_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__WishList__4F227CA0D99D4B73", x => x.wish_id);
                    table.ForeignKey(
                        name: "FKWishList705462",
                        column: x => x.cust_id,
                        principalTable: "Customer",
                        principalColumn: "cust_id");
                    table.ForeignKey(
                        name: "FKWishList795792",
                        column: x => x.merchandise_id,
                        principalTable: "Merchandise",
                        principalColumn: "merch_id");
                    table.ForeignKey(
                        name: "FKWishList952486",
                        column: x => x.game_id,
                        principalTable: "Game",
                        principalColumn: "game_id");
                });

            migrationBuilder.CreateTable(
                name: "ReviewImage",
                columns: table => new
                {
                    reviewImg_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    path = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    extention = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    review_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ReviewIm__ADCB53FF60CE66D7", x => x.reviewImg_id);
                    table.ForeignKey(
                        name: "FKReviewImag856029",
                        column: x => x.review_id,
                        principalTable: "Review",
                        principalColumn: "review_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CreditCard_cust_id",
                table: "CreditCard",
                column: "cust_id");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_preferedCategory_id",
                table: "Customer",
                column: "preferedCategory_id");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_preferedPlatform_id",
                table: "Customer",
                column: "preferedPlatform_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Customer__AB6E6164661522CF",
                table: "Customer",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Customer__CC6CD17E283CABFD",
                table: "Customer",
                column: "Nickname",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_Event_Eventid",
                table: "Customer_Event",
                column: "Eventid");

            migrationBuilder.CreateIndex(
                name: "IX_FriendsFamily_cust_id1",
                table: "FriendsFamily",
                column: "cust_id1");

            migrationBuilder.CreateIndex(
                name: "IX_FriendsFamily_cust_id2",
                table: "FriendsFamily",
                column: "cust_id2");

            migrationBuilder.CreateIndex(
                name: "IX_Game_Category_Categoryid",
                table: "Game_Category",
                column: "Categoryid");

            migrationBuilder.CreateIndex(
                name: "IX_GameFeature_Game_Gameid",
                table: "GameFeature_Game",
                column: "Gameid");

            migrationBuilder.CreateIndex(
                name: "IX_GameImage_game_id",
                table: "GameImage",
                column: "game_id");

            migrationBuilder.CreateIndex(
                name: "IX_MerchandiseImage_merchandise_id",
                table: "MerchandiseImage",
                column: "merchandise_id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_cust_id",
                table: "Order",
                column: "cust_id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_game_id",
                table: "Order",
                column: "game_id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_merchandise_id",
                table: "Order",
                column: "merchandise_id");

            migrationBuilder.CreateIndex(
                name: "IX_Platform_Game_Gameid",
                table: "Platform_Game",
                column: "Gameid");

            migrationBuilder.CreateIndex(
                name: "IX_Review_cust_id",
                table: "Review",
                column: "cust_id");

            migrationBuilder.CreateIndex(
                name: "IX_Review_game_id",
                table: "Review",
                column: "game_id");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewImage_review_id",
                table: "ReviewImage",
                column: "review_id");

            migrationBuilder.CreateIndex(
                name: "IX_WishList_cust_id",
                table: "WishList",
                column: "cust_id");

            migrationBuilder.CreateIndex(
                name: "IX_WishList_game_id",
                table: "WishList",
                column: "game_id");

            migrationBuilder.CreateIndex(
                name: "IX_WishList_merchandise_id",
                table: "WishList",
                column: "merchandise_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryGame");

            migrationBuilder.DropTable(
                name: "CreditCard");

            migrationBuilder.DropTable(
                name: "Customer_Event");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "FriendsFamily");

            migrationBuilder.DropTable(
                name: "Game_Category");

            migrationBuilder.DropTable(
                name: "GameFeature_Game");

            migrationBuilder.DropTable(
                name: "GameGameFeature");

            migrationBuilder.DropTable(
                name: "GameImage");

            migrationBuilder.DropTable(
                name: "GamePlatform");

            migrationBuilder.DropTable(
                name: "MerchandiseImage");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Platform_Game");

            migrationBuilder.DropTable(
                name: "ReviewImage");

            migrationBuilder.DropTable(
                name: "WishList");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "GameFeature");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "Merchandise");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "Platform");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
