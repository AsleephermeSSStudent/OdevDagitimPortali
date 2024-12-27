using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OdevDagitimUI.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "456666db-e450-4fa2-b32b-612940949dfb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "99d92d6a-fc75-45e6-ab21-eff1f5a96bae");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "da4fe665-f376-46ed-85a8-898ad781a3c4", "4f1f9c81-43b3-4597-b4fd-55bf6774a6d5" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "da4fe665-f376-46ed-85a8-898ad781a3c4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4f1f9c81-43b3-4597-b4fd-55bf6774a6d5");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Created", "Name", "NormalizedName", "Updated" },
                values: new object[,]
                {
                    { "184d06a1-99f8-42d8-8d72-921cd6a74ac0", null, new DateTime(2024, 12, 27, 15, 34, 24, 448, DateTimeKind.Local).AddTicks(8163), "Ogrenci", "OGRENCI", new DateTime(2024, 12, 27, 15, 34, 24, 448, DateTimeKind.Local).AddTicks(8163) },
                    { "5e890959-532f-45a4-9942-d257f5a69cb2", null, new DateTime(2024, 12, 27, 15, 34, 24, 448, DateTimeKind.Local).AddTicks(8160), "Teacher", "TEACHER", new DateTime(2024, 12, 27, 15, 34, 24, 448, DateTimeKind.Local).AddTicks(8161) },
                    { "6f579cd3-9055-4f11-baf6-7329571c369f", null, new DateTime(2024, 12, 27, 15, 34, 24, 448, DateTimeKind.Local).AddTicks(8145), "admin", "ADMIN", new DateTime(2024, 12, 27, 15, 34, 24, 448, DateTimeKind.Local).AddTicks(8158) }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ClassId", "ClassId1", "ConcurrencyStamp", "Created", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Surname", "TwoFactorEnabled", "Updated", "UserName" },
                values: new object[] { "c2a11213-bec9-4738-b55c-a3a5973f9c2d", 0, null, null, "974bc251-5b2a-40d5-bfb5-3454838aac19", new DateTime(2024, 12, 27, 15, 34, 24, 490, DateTimeKind.Local).AddTicks(1892), "admin@example.com", true, false, null, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEB938lfQYimmtgRm18p0y4yFJkQ/JvBMzQ6tq0MTdYldIvV5d6m2S/qvKkRGlU6Z0Q==", null, false, "", null, false, new DateTime(2024, 12, 27, 15, 34, 24, 490, DateTimeKind.Local).AddTicks(1903), "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "6f579cd3-9055-4f11-baf6-7329571c369f", "c2a11213-bec9-4738-b55c-a3a5973f9c2d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "184d06a1-99f8-42d8-8d72-921cd6a74ac0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e890959-532f-45a4-9942-d257f5a69cb2");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6f579cd3-9055-4f11-baf6-7329571c369f", "c2a11213-bec9-4738-b55c-a3a5973f9c2d" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6f579cd3-9055-4f11-baf6-7329571c369f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c2a11213-bec9-4738-b55c-a3a5973f9c2d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Created", "Name", "NormalizedName", "Updated" },
                values: new object[,]
                {
                    { "456666db-e450-4fa2-b32b-612940949dfb", null, new DateTime(2024, 12, 19, 17, 58, 39, 544, DateTimeKind.Local).AddTicks(7290), "Teacher", "TEACHER", new DateTime(2024, 12, 19, 17, 58, 39, 544, DateTimeKind.Local).AddTicks(7290) },
                    { "99d92d6a-fc75-45e6-ab21-eff1f5a96bae", null, new DateTime(2024, 12, 19, 17, 58, 39, 544, DateTimeKind.Local).AddTicks(7300), "Ogrenci", "OGRENCI", new DateTime(2024, 12, 19, 17, 58, 39, 544, DateTimeKind.Local).AddTicks(7300) },
                    { "da4fe665-f376-46ed-85a8-898ad781a3c4", null, new DateTime(2024, 12, 19, 17, 58, 39, 544, DateTimeKind.Local).AddTicks(7275), "admin", "ADMIN", new DateTime(2024, 12, 19, 17, 58, 39, 544, DateTimeKind.Local).AddTicks(7287) }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ClassId", "ClassId1", "ConcurrencyStamp", "Created", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Surname", "TwoFactorEnabled", "Updated", "UserName" },
                values: new object[] { "4f1f9c81-43b3-4597-b4fd-55bf6774a6d5", 0, null, null, "1faab7d5-d895-4183-a94c-48ce445039c1", new DateTime(2024, 12, 19, 17, 58, 39, 588, DateTimeKind.Local).AddTicks(8523), "admin@example.com", true, false, null, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEE6m/70OCd+IgLmmCOmM/EIjUCS45bVag6ZYTqGrUWjNWQvjQYrE/INBWJhBm7sBUQ==", null, false, "", null, false, new DateTime(2024, 12, 19, 17, 58, 39, 588, DateTimeKind.Local).AddTicks(8535), "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "da4fe665-f376-46ed-85a8-898ad781a3c4", "4f1f9c81-43b3-4597-b4fd-55bf6774a6d5" });
        }
    }
}
