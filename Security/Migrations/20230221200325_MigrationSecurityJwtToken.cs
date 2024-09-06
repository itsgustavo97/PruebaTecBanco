using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Security.Migrations
{
    public partial class MigrationSecurityJwtToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "79ba8e3f-5c28-42cb-a03e-babcfb0b5bd8", "93986ce3-1133-43bf-b969-cf8db42d5cca", "Administrator", "ADMINISTRATOR" },
                    { "8c26c17c-ffe7-43ad-a3b3-b6d50ca71a63", "1cbcedb2-870e-4b01-9d82-10dacd9f760d", "Operator", "OPERATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Nombre", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "294d249b-9b57-48c1-9689-11a91abb6447", 0, "c0c858a8-6ed2-4ee4-b86f-ad69fe83f251", "juanperez@locahost.com", true, false, null, "Juan", "juanperez@locahost.com", "juanperez", "AQAAAAEAACcQAAAAEGnygiR7mwq8/7vqdtdhXbAtE0j1U2Fhf1EhxuZpOjz//mT7wBE6+9UI+0tVLAjf6w==", null, false, "16e09d94-9508-4a4b-816b-c7fac08e6675", false, "juanperez" },
                    { "f284b3fd-f2cf-476e-a9b6-6560689cc48c", 0, "e0affe9c-6ad9-471a-bb33-1fa1c44c6f6d", "admin@locahost.com", true, false, null, "Vaxi", "admin@locahost.com", "vaxidrez", "AQAAAAEAACcQAAAAEEs8QaWRXRcDCinmhGIRBQ3hSYK4Rbl5mnAD0ltUWJpdCxvgjCkoGGu54iH5EU5mkg==", null, false, "662f8ac1-a094-4f18-a55c-a972e4b44a9d", false, "vaxidrez" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "8c26c17c-ffe7-43ad-a3b3-b6d50ca71a63", "294d249b-9b57-48c1-9689-11a91abb6447" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "79ba8e3f-5c28-42cb-a03e-babcfb0b5bd8", "f284b3fd-f2cf-476e-a9b6-6560689cc48c" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8c26c17c-ffe7-43ad-a3b3-b6d50ca71a63", "294d249b-9b57-48c1-9689-11a91abb6447" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "79ba8e3f-5c28-42cb-a03e-babcfb0b5bd8", "f284b3fd-f2cf-476e-a9b6-6560689cc48c" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "79ba8e3f-5c28-42cb-a03e-babcfb0b5bd8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8c26c17c-ffe7-43ad-a3b3-b6d50ca71a63");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "294d249b-9b57-48c1-9689-11a91abb6447");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f284b3fd-f2cf-476e-a9b6-6560689cc48c");
        }
    }
}
