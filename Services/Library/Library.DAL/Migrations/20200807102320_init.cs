using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.DAL.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Instruments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instruments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LibraryUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdentityUserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sheets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: false),
                    IsCurrent = table.Column<bool>(nullable: false),
                    Composer = table.Column<string>(nullable: true),
                    Arranger = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: true),
                    IsIndependance = table.Column<bool>(nullable: false),
                    IsGarde = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sheets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sheets_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserInstruments",
                columns: table => new
                {
                    LibUserId = table.Column<int>(nullable: false),
                    InstrumentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInstruments", x => new { x.LibUserId, x.InstrumentId });
                    table.ForeignKey(
                        name: "FK_UserInstruments_Instruments_InstrumentId",
                        column: x => x.InstrumentId,
                        principalTable: "Instruments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserInstruments_LibraryUsers_LibUserId",
                        column: x => x.LibUserId,
                        principalTable: "LibraryUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SheetParts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InstrumentId = table.Column<int>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    SheetId = table.Column<int>(nullable: false),
                    Part = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SheetParts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SheetParts_Instruments_InstrumentId",
                        column: x => x.InstrumentId,
                        principalTable: "Instruments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SheetParts_Sheets_SheetId",
                        column: x => x.SheetId,
                        principalTable: "Sheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Instruments",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Direction" });

            migrationBuilder.InsertData(
                table: "Instruments",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Flûte/Piccolo" });

            migrationBuilder.InsertData(
                table: "Instruments",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Hautbois" });

            migrationBuilder.InsertData(
                table: "Instruments",
                columns: new[] { "Id", "Name" },
                values: new object[] { 4, "Trompette/Cornet" });

            migrationBuilder.InsertData(
                table: "Instruments",
                columns: new[] { "Id", "Name" },
                values: new object[] { 5, "Bugle" });

            migrationBuilder.InsertData(
                table: "Instruments",
                columns: new[] { "Id", "Name" },
                values: new object[] { 6, "Clarinette" });

            migrationBuilder.InsertData(
                table: "Instruments",
                columns: new[] { "Id", "Name" },
                values: new object[] { 7, "Saxophone" });

            migrationBuilder.InsertData(
                table: "Instruments",
                columns: new[] { "Id", "Name" },
                values: new object[] { 8, "Cor" });

            migrationBuilder.InsertData(
                table: "Instruments",
                columns: new[] { "Id", "Name" },
                values: new object[] { 9, "Baryton/Euphonium" });

            migrationBuilder.InsertData(
                table: "Instruments",
                columns: new[] { "Id", "Name" },
                values: new object[] { 10, "Trombone/Buccin/Serpent" });

            migrationBuilder.InsertData(
                table: "Instruments",
                columns: new[] { "Id", "Name" },
                values: new object[] { 11, "Clarinette Basse" });

            migrationBuilder.InsertData(
                table: "Instruments",
                columns: new[] { "Id", "Name" },
                values: new object[] { 12, "Basson" });

            migrationBuilder.InsertData(
                table: "Instruments",
                columns: new[] { "Id", "Name" },
                values: new object[] { 13, "Tuba" });

            migrationBuilder.InsertData(
                table: "Instruments",
                columns: new[] { "Id", "Name" },
                values: new object[] { 14, "Basse Ut" });

            migrationBuilder.InsertData(
                table: "Instruments",
                columns: new[] { "Id", "Name" },
                values: new object[] { 15, "Percussions" });

            migrationBuilder.InsertData(
                table: "Instruments",
                columns: new[] { "Id", "Name" },
                values: new object[] { 16, "Autre" });

            migrationBuilder.CreateIndex(
                name: "IX_SheetParts_InstrumentId",
                table: "SheetParts",
                column: "InstrumentId");

            migrationBuilder.CreateIndex(
                name: "IX_SheetParts_SheetId",
                table: "SheetParts",
                column: "SheetId");

            migrationBuilder.CreateIndex(
                name: "IX_Sheets_CategoryId",
                table: "Sheets",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInstruments_InstrumentId",
                table: "UserInstruments",
                column: "InstrumentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SheetParts");

            migrationBuilder.DropTable(
                name: "UserInstruments");

            migrationBuilder.DropTable(
                name: "Sheets");

            migrationBuilder.DropTable(
                name: "Instruments");

            migrationBuilder.DropTable(
                name: "LibraryUsers");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
