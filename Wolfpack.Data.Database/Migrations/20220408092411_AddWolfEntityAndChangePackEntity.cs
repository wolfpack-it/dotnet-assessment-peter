using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wolfpack.Data.Database.Migrations
{
    public partial class AddWolfEntityAndChangePackEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Packs");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Packs");

            migrationBuilder.CreateTable(
                name: "Wolves",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(10,7)", precision: 10, scale: 7, nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(10,7)", precision: 10, scale: 7, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wolves", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PackWolf",
                columns: table => new
                {
                    PacksId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WolvesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackWolf", x => new { x.PacksId, x.WolvesId });
                    table.ForeignKey(
                        name: "FK_PackWolf_Packs_PacksId",
                        column: x => x.PacksId,
                        principalTable: "Packs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PackWolf_Wolves_WolvesId",
                        column: x => x.WolvesId,
                        principalTable: "Wolves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PackWolf_WolvesId",
                table: "PackWolf",
                column: "WolvesId");

            migrationBuilder.CreateIndex(
                name: "IX_Wolves_Name",
                table: "Wolves",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PackWolf");

            migrationBuilder.DropTable(
                name: "Wolves");

            migrationBuilder.AddColumn<decimal>(
                name: "Latitude",
                table: "Packs",
                type: "decimal(10,7)",
                precision: 10,
                scale: 7,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Longitude",
                table: "Packs",
                type: "decimal(10,7)",
                precision: 10,
                scale: 7,
                nullable: false,
                defaultValue: 0m);
        }
    }
}
