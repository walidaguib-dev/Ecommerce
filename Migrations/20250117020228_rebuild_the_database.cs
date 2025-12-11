using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class rebuild_the_database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9b4c876c-9b24-45c0-993a-84b331ef2dbe");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fbb3ee08-2681-4e9e-b3c5-bf95bdae5544");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b27e77bb-3edb-4ca8-8684-4fc4f35dd2c2", null, "user", "USER" },
                    { "e8e804be-471d-4b55-9a58-4060d42beb55", null, "admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b27e77bb-3edb-4ca8-8684-4fc4f35dd2c2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e8e804be-471d-4b55-9a58-4060d42beb55");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9b4c876c-9b24-45c0-993a-84b331ef2dbe", null, "admin", "ADMIN" },
                    { "fbb3ee08-2681-4e9e-b3c5-bf95bdae5544", null, "user", "USER" }
                });
        }
    }
}
