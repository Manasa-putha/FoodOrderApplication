using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FOABE.Migrations
{
    public partial class migrse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cartitems_Menus_MenuId",
                table: "Cartitems");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2024, 10, 14, 23, 31, 21, 925, DateTimeKind.Local).AddTicks(121));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2024, 10, 14, 23, 31, 21, 925, DateTimeKind.Local).AddTicks(123));

            migrationBuilder.AddForeignKey(
                name: "FK_Cartitems_Menus_MenuId",
                table: "Cartitems",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "MenuId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cartitems_Menus_MenuId",
                table: "Cartitems");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2024, 10, 14, 23, 27, 5, 930, DateTimeKind.Local).AddTicks(9555));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2024, 10, 14, 23, 27, 5, 930, DateTimeKind.Local).AddTicks(9557));

            migrationBuilder.AddForeignKey(
                name: "FK_Cartitems_Menus_MenuId",
                table: "Cartitems",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "MenuId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
