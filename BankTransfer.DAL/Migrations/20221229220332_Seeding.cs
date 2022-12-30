using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BankTransfer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Organization",
                columns: new[] { "ClientId", "Address", "ClientSecret", "CreatedBy", "CreatedOn", "Deleted", "Email", "Name", "UpdatedBy", "UpdatedOn" },
                values: new object[] { 1, "14, Yeye Olofin Street, Lekki Phase 1, Lagos, Nigeria", "YT33#@*(^%@@!#EWADE#", null, new DateTime(2022, 12, 29, 23, 3, 32, 221, DateTimeKind.Local).AddTicks(5033), false, "info@innovectives.com", "Index", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Provider",
                columns: new[] { "ProviderId", "CreatedBy", "CreatedOn", "Deleted", "Name", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2022, 12, 29, 23, 3, 32, 221, DateTimeKind.Local).AddTicks(5249), false, "Paystack", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, null, new DateTime(2022, 12, 29, 23, 3, 32, 221, DateTimeKind.Local).AddTicks(5251), false, "Flutterwave", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Organization",
                keyColumn: "ClientId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Provider",
                keyColumn: "ProviderId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Provider",
                keyColumn: "ProviderId",
                keyValue: 2);
        }
    }
}
