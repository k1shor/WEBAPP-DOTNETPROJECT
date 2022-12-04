using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WEBAPP.Migrations
{
    public partial class newdb3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewOrders_AspNetUsers_ApplicationUserId",
                table: "NewOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_NewOrders_Products_ProductId",
                table: "NewOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewOrders",
                table: "NewOrders");

            migrationBuilder.RenameTable(
                name: "NewOrders",
                newName: "Orders");

            migrationBuilder.RenameIndex(
                name: "IX_NewOrders_ProductId",
                table: "Orders",
                newName: "IX_Orders_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_NewOrders_ApplicationUserId",
                table: "Orders",
                newName: "IX_Orders_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_ApplicationUserId",
                table: "Orders",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Products_ProductId",
                table: "Orders",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "product_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_ApplicationUserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Products_ProductId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "NewOrders");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_ProductId",
                table: "NewOrders",
                newName: "IX_NewOrders_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_ApplicationUserId",
                table: "NewOrders",
                newName: "IX_NewOrders_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewOrders",
                table: "NewOrders",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_NewOrders_AspNetUsers_ApplicationUserId",
                table: "NewOrders",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NewOrders_Products_ProductId",
                table: "NewOrders",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "product_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
