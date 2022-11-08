using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameStore.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameCategory");

            migrationBuilder.DropTable(
                name: "GameFeatureGame");

            migrationBuilder.DropTable(
                name: "PlatformGame");

            migrationBuilder.CreateTable(
                name: "GameCategory",
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
                name: "GameFeatureGame",
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
                name: "PlatformGame",
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

            migrationBuilder.CreateIndex(
                name: "IX_GameCategory_Categoryid",
                table: "GameCategory",
                column: "Categoryid");

            migrationBuilder.CreateIndex(
                name: "IX_GameFeatureGame_Gameid",
                table: "GameFeatureGame",
                column: "Gameid");

            migrationBuilder.CreateIndex(
                name: "IX_PlatformGame_Gameid",
                table: "PlatformGame",
                column: "Gameid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameCategory");

            migrationBuilder.DropTable(
                name: "GameFeatureGame");

            migrationBuilder.DropTable(
                name: "PlatformGame");

            migrationBuilder.CreateTable(
                name: "CategoryGame",
                columns: table => new
                {
                    CategoriesCategoryId = table.Column<int>(type: "int", nullable: false),
                    GamesGameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryGame", x => new { x.CategoriesCategoryId, x.GamesGameId });
                    table.ForeignKey(
                        name: "FK_CategoryGame_Category_CategoriesCategoryId",
                        column: x => x.CategoriesCategoryId,
                        principalTable: "Category",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryGame_Game_GamesGameId",
                        column: x => x.GamesGameId,
                        principalTable: "Game",
                        principalColumn: "game_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameGameFeature",
                columns: table => new
                {
                    GameFeaturesFeatureId = table.Column<int>(type: "int", nullable: false),
                    GamesGameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameGameFeature", x => new { x.GameFeaturesFeatureId, x.GamesGameId });
                    table.ForeignKey(
                        name: "FK_GameGameFeature_Game_GamesGameId",
                        column: x => x.GamesGameId,
                        principalTable: "Game",
                        principalColumn: "game_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameGameFeature_GameFeature_GameFeaturesFeatureId",
                        column: x => x.GameFeaturesFeatureId,
                        principalTable: "GameFeature",
                        principalColumn: "feature_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GamePlatform",
                columns: table => new
                {
                    GamesGameId = table.Column<int>(type: "int", nullable: false),
                    PlatformsPlatformId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePlatform", x => new { x.GamesGameId, x.PlatformsPlatformId });
                    table.ForeignKey(
                        name: "FK_GamePlatform_Game_GamesGameId",
                        column: x => x.GamesGameId,
                        principalTable: "Game",
                        principalColumn: "game_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamePlatform_Platform_PlatformsPlatformId",
                        column: x => x.PlatformsPlatformId,
                        principalTable: "Platform",
                        principalColumn: "platform_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryGame_GamesGameId",
                table: "CategoryGame",
                column: "GamesGameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameGameFeature_GamesGameId",
                table: "GameGameFeature",
                column: "GamesGameId");

            migrationBuilder.CreateIndex(
                name: "IX_GamePlatform_PlatformsPlatformId",
                table: "GamePlatform",
                column: "PlatformsPlatformId");
        }
    }
}
