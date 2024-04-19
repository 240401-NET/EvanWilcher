using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_Project.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trainer",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Trainer__3214EC2797527C1D", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PokemonTeam",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TrainerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PokemonT__3214EC27AE892B21", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TrainerID_Team",
                        column: x => x.TrainerID,
                        principalTable: "Trainer",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Pokemon",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    TeamID = table.Column<int>(type: "int", nullable: false),
                    ChosenAbility = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsShiny = table.Column<bool>(type: "bit", nullable: false),
                    Level = table.Column<byte>(type: "tinyint", nullable: false),
                    ChosenTeraType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Pokemon__3214EC27E8401125", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Pokemon__TeamID__06CD04F7",
                        column: x => x.TeamID,
                        principalTable: "PokemonTeam",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PokemonAbility",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsHidden = table.Column<bool>(type: "bit", nullable: false),
                    Slot = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    URL = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PokemonID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PokemonA__3214EC27B1C24632", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PokemonID_Ability",
                        column: x => x.PokemonID,
                        principalTable: "Pokemon",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PokemonHeldItem",
                columns: table => new
                {
                    PokemonID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Sprite = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonHeldItem", x => x.PokemonID);
                    table.ForeignKey(
                        name: "FK_PokemonID_Item",
                        column: x => x.PokemonID,
                        principalTable: "Pokemon",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PokemonMove",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    URL = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PokemonID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PokemonM__3214EC27777892D0", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PokemonID_Move",
                        column: x => x.PokemonID,
                        principalTable: "Pokemon",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PokemonMoveSet",
                columns: table => new
                {
                    PokemonID = table.Column<int>(type: "int", nullable: false),
                    move_1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    move_2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    move_3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    move_ = table.Column<string>(name: "move_#", type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoveSet", x => x.PokemonID);
                    table.ForeignKey(
                        name: "FK_PokemonID_MoveSet",
                        column: x => x.PokemonID,
                        principalTable: "Pokemon",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PokemonSprite",
                columns: table => new
                {
                    PokemonID = table.Column<int>(type: "int", nullable: false),
                    FrontDefault = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FrontShiny = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FrontFemale = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FrontShinyFemale = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonSprite", x => x.PokemonID);
                    table.ForeignKey(
                        name: "FK_PokemonID_Sprite",
                        column: x => x.PokemonID,
                        principalTable: "Pokemon",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PokemonStat",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Effort = table.Column<int>(type: "int", nullable: false),
                    Individual = table.Column<int>(type: "int", nullable: false),
                    BaseStat = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    URL = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PokemonID = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonStat", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PokemonID_Stat",
                        column: x => x.PokemonID,
                        principalTable: "Pokemon",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PokemonType",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Slot = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PokemonID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PokemonT__3214EC27461BAB9A", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PokemonID_Type",
                        column: x => x.PokemonID,
                        principalTable: "Pokemon",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pokemon_TeamID",
                table: "Pokemon",
                column: "TeamID");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonAbility_PokemonID",
                table: "PokemonAbility",
                column: "PokemonID");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonMove_PokemonID",
                table: "PokemonMove",
                column: "PokemonID");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonStat_PokemonID",
                table: "PokemonStat",
                column: "PokemonID");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonTeam_TrainerID",
                table: "PokemonTeam",
                column: "TrainerID");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonType_PokemonID",
                table: "PokemonType",
                column: "PokemonID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PokemonAbility");

            migrationBuilder.DropTable(
                name: "PokemonHeldItem");

            migrationBuilder.DropTable(
                name: "PokemonMove");

            migrationBuilder.DropTable(
                name: "PokemonMoveSet");

            migrationBuilder.DropTable(
                name: "PokemonSprite");

            migrationBuilder.DropTable(
                name: "PokemonStat");

            migrationBuilder.DropTable(
                name: "PokemonType");

            migrationBuilder.DropTable(
                name: "Pokemon");

            migrationBuilder.DropTable(
                name: "PokemonTeam");

            migrationBuilder.DropTable(
                name: "Trainer");
        }
    }
}
