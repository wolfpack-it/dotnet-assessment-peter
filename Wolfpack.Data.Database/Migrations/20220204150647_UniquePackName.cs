using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wolfpack.Data.Database.Migrations
{
    public partial class UniquePackName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Packs_Name",
                table: "Packs",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Packs_Name",
                table: "Packs");
        }
    }
}
