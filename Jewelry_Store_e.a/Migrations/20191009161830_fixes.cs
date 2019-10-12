using Microsoft.EntityFrameworkCore.Migrations;

namespace Jewelry_Store_e.a.Migrations
{
    public partial class fixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseProduct_Orders_OrderID",
                table: "PurchaseProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseProduct_Products_ProductID",
                table: "PurchaseProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseProduct",
                table: "PurchaseProduct");

            migrationBuilder.RenameTable(
                name: "PurchaseProduct",
                newName: "PurchaseProducts");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseProduct_ProductID",
                table: "PurchaseProducts",
                newName: "IX_PurchaseProducts_ProductID");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseProduct_OrderID",
                table: "PurchaseProducts",
                newName: "IX_PurchaseProducts_OrderID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseProducts",
                table: "PurchaseProducts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseProducts_Orders_OrderID",
                table: "PurchaseProducts",
                column: "OrderID",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseProducts_Products_ProductID",
                table: "PurchaseProducts",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseProducts_Orders_OrderID",
                table: "PurchaseProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseProducts_Products_ProductID",
                table: "PurchaseProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseProducts",
                table: "PurchaseProducts");

            migrationBuilder.RenameTable(
                name: "PurchaseProducts",
                newName: "PurchaseProduct");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseProducts_ProductID",
                table: "PurchaseProduct",
                newName: "IX_PurchaseProduct_ProductID");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseProducts_OrderID",
                table: "PurchaseProduct",
                newName: "IX_PurchaseProduct_OrderID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseProduct",
                table: "PurchaseProduct",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseProduct_Orders_OrderID",
                table: "PurchaseProduct",
                column: "OrderID",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseProduct_Products_ProductID",
                table: "PurchaseProduct",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
