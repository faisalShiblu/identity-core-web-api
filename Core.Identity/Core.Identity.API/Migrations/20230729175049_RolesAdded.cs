using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Core.Identity.API.Migrations
{
    /// <inheritdoc />
    public partial class RolesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "761af1d7-e8b1-4ffc-ae37-56df80d697f4", "c373a316-9065-499c-8e97-6493e28b7c99", "User", "USER" },
                    { "844d7d57-f6ee-4bc9-9257-3051da677c6e", "3c1b6fdb-32aa-4c6b-8c12-96a8ef50d03f", "Admin", "ADMIN" },
                    { "d2db01ca-23f6-4fe7-bfd7-f4bd55cda440", "687718a7-04bf-454e-a4ff-90823fe4e057", "Manager", "MANAGER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "761af1d7-e8b1-4ffc-ae37-56df80d697f4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "844d7d57-f6ee-4bc9-9257-3051da677c6e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d2db01ca-23f6-4fe7-bfd7-f4bd55cda440");
        }
    }
}
