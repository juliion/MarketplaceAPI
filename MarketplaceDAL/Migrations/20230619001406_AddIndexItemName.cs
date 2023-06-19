using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketplaceDAL.Migrations
{
    public partial class AddIndexItemName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Items_Name",
                table: "Items",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Items_Name",
                table: "Items");
        }
    }
}
