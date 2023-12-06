using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackRowStore.Migrations
{
    /// <inheritdoc />
    public partial class CartSerialOBJ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Carts_cartID",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_cartID",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Carts",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "cartID",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "cartID",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "cartBalance",
                table: "Carts");

            migrationBuilder.RenameColumn(
                name: "itemSerial",
                table: "Carts",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "cartSerial",
                table: "Carts",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carts",
                table: "Carts",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Carts",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "cartSerial",
                table: "Carts");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Carts",
                newName: "itemSerial");

            migrationBuilder.AddColumn<string>(
                name: "cartID",
                table: "Items",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "cartID",
                table: "Carts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "cartBalance",
                table: "Carts",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carts",
                table: "Carts",
                column: "cartID");

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
    }
}
