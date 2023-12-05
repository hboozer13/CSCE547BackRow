using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackRowStore.Migrations
{
    /// <inheritdoc />
    public partial class AddSerialToCartTest4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "itemSerial",
                table: "Carts",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "itemSerial",
                table: "Carts");
        }
    }
}
