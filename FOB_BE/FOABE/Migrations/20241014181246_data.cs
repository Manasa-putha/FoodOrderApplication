using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FOABE.Migrations
{
    public partial class data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
