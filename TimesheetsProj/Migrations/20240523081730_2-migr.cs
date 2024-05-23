using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimesheetsProj.Migrations
{
    /// <inheritdoc />
    public partial class _2migr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "User",
                table: "clients",
                newName: "UserId");

            migrationBuilder.AddColumn<Guid>(
                name: "ClientId",
                table: "contracts",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_contracts_ClientId",
                table: "contracts",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_contracts_clients_ClientId",
                table: "contracts",
                column: "ClientId",
                principalTable: "clients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_contracts_clients_ClientId",
                table: "contracts");

            migrationBuilder.DropIndex(
                name: "IX_contracts_ClientId",
                table: "contracts");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "contracts");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "clients",
                newName: "User");
        }
    }
}
