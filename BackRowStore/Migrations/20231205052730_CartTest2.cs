using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackRowStore.Migrations
{
    /// <inheritdoc />
    public partial class CartTest2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "cartID",
                table: "Items",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    cartID = table.Column<string>(type: "text", nullable: false),
                    cartBalance = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.cartID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_cartID",
                table: "Items",
                column: "cartID");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Carts_cartID",
                table: "Items",
                column: "cartID",
                principalTable: "Carts",
                principalColumn: "cartID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Carts_cartID",
                table: "Items");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Items_cartID",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "cartID",
                table: "Items");
        }
    }
}
