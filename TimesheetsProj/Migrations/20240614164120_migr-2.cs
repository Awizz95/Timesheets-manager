using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimesheetsProj.Migrations
{
    /// <inheritdoc />
    public partial class migr2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpireTime",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: new Guid("109f4988-c1cb-4845-b6c5-5a824cbcd338"),
                columns: new[] { "RefreshToken", "RefreshTokenExpireTime" },
                values: new object[] { null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: new Guid("1744b83d-059e-4973-9c4f-0781c03f1079"),
                columns: new[] { "RefreshToken", "RefreshTokenExpireTime" },
                values: new object[] { null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: new Guid("769c84d7-01bd-4e4f-aeac-ef4963e9bd84"),
                columns: new[] { "RefreshToken", "RefreshTokenExpireTime" },
                values: new object[] { null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: new Guid("d0d43c64-ae2b-4518-a41c-8d24031abcc5"),
                columns: new[] { "RefreshToken", "RefreshTokenExpireTime" },
                values: new object[] { null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "users");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpireTime",
                table: "users");
        }
    }
}
