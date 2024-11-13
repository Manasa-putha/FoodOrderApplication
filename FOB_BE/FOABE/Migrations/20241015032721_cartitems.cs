using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FOABE.Migrations
{
    public partial class cartitems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Cartitems",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2024, 10, 15, 8, 57, 21, 433, DateTimeKind.Local).AddTicks(3648));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2024, 10, 15, 8, 57, 21, 433, DateTimeKind.Local).AddTicks(3650));

            migrationBuilder.CreateIndex(
                name: "IX_Cartitems_OrderId",
                table: "Cartitems",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cartitems_Orders_OrderId",
                table: "Cartitems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cartitems_Orders_OrderId",
                table: "Cartitems");

            migrationBuilder.DropIndex(
                name: "IX_Cartitems_OrderId",
                table: "Cartitems");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Cartitems");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2024, 10, 14, 23, 42, 45, 881, DateTimeKind.Local).AddTicks(2560));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2024, 10, 14, 23, 42, 45, 881, DateTimeKind.Local).AddTicks(2561));
        }
    }
}
