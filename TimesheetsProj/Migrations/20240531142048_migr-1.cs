﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TimesheetsProj.Migrations
{
    /// <inheritdoc />
    public partial class migr1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Money",
                columns: table => new
                {
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "services",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Cost = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "userroles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userroles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "bytea", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "contracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    DateStart = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateEnd = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_contracts_clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "clients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "invoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ContractId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateStart = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateEnd = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_invoices_contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sheets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    ContractId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uuid", nullable: true),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: false),
                    ApprovedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sheets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sheets_contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sheets_employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sheets_invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "invoices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_sheets_services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "clients",
                columns: new[] { "Id", "IsDeleted", "UserId" },
                values: new object[] { new Guid("3287389a-acf3-4d19-b7ee-b19a89396b23"), false, new Guid("d0d43c64-ae2b-4518-a41c-8d24031abcc5") });

            migrationBuilder.InsertData(
                table: "contracts",
                columns: new[] { "Id", "ClientId", "DateEnd", "DateStart", "Description", "IsDeleted", "Title" },
                values: new object[,]
                {
                    { new Guid("28c08503-c932-4160-aa41-a9cffa1fc630"), null, new DateTime(2024, 12, 31, 23, 59, 0, 0, DateTimeKind.Utc), new DateTime(2024, 5, 20, 12, 0, 0, 0, DateTimeKind.Utc), "Описание контракта #1", false, "Тестовый действующий контракт #1" },
                    { new Guid("8ed5761c-858e-4106-a689-168c4e59c5d4"), null, new DateTime(2024, 4, 5, 10, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 2, 10, 11, 0, 0, 0, DateTimeKind.Utc), "Описание контракта #4", false, "Тестовый истекший контракт #4" },
                    { new Guid("b0c752f7-3a52-4f80-8b71-0b4eef05396b"), null, new DateTime(2026, 5, 31, 23, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 10, 11, 0, 0, 0, DateTimeKind.Utc), "Описание контракта #3", false, "Тестовый действующий контракт #3" },
                    { new Guid("d6050cad-666d-43c0-9443-4ae7e7fd6a51"), null, new DateTime(2025, 12, 31, 23, 59, 0, 0, DateTimeKind.Utc), new DateTime(2024, 3, 24, 23, 0, 0, 0, DateTimeKind.Utc), "Описание контракта #2", false, "Тестовый действующий контракт #2" }
                });

            migrationBuilder.InsertData(
                table: "employees",
                columns: new[] { "Id", "IsDeleted", "UserId" },
                values: new object[] { new Guid("10e63f7e-0bb8-46f7-a27a-411d9140cafc"), false, new Guid("769c84d7-01bd-4e4f-aeac-ef4963e9bd84") });

            migrationBuilder.InsertData(
                table: "services",
                columns: new[] { "Id", "Cost", "Name" },
                values: new object[,]
                {
                    { new Guid("3647ae1a-818d-43db-9c5e-9027b0a78e33"), 155.555m, "Тестовый сервис #3" },
                    { new Guid("764d9967-651d-4425-8237-8005b2f1ca32"), 10.877m, "Тестовый сервис #2" },
                    { new Guid("e0521823-8640-45d1-9de8-0f2e01102b83"), 5.435m, "Тестовый сервис #1" }
                });

            migrationBuilder.InsertData(
                table: "userroles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("75016735-84f3-46f7-a2f2-fd55815f09d2"), "Client" },
                    { new Guid("92287388-b5a8-48af-8497-a137dd47ac58"), "User" },
                    { new Guid("c3382a38-019c-4a62-bfbd-cba4c6b8e229"), "Admin" },
                    { new Guid("f9e490fc-c09e-459c-bae3-194d64df2015"), "Employee" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("109f4988-c1cb-4845-b6c5-5a824cbcd338"), new byte[] { 94, 119, 237, 33, 80, 41, 39, 154, 45, 0, 255, 133, 53, 50, 146, 176, 48, 208, 107, 156 }, "User", "Max" },
                    { new Guid("1744b83d-059e-4973-9c4f-0781c03f1079"), new byte[] { 106, 110, 54, 11, 228, 45, 74, 106, 2, 195, 249, 195, 183, 129, 135, 24, 46, 159, 106, 145 }, "Admin", "Andrey" },
                    { new Guid("769c84d7-01bd-4e4f-aeac-ef4963e9bd84"), new byte[] { 103, 241, 94, 178, 185, 71, 163, 213, 221, 106, 1, 99, 200, 0, 142, 40, 143, 26, 239, 114 }, "Employee", "Alex" },
                    { new Guid("d0d43c64-ae2b-4518-a41c-8d24031abcc5"), new byte[] { 172, 159, 122, 37, 13, 70, 11, 221, 231, 73, 199, 12, 190, 87, 38, 231, 139, 172, 169, 221 }, "Client", "Mark" }
                });

            migrationBuilder.InsertData(
                table: "invoices",
                columns: new[] { "Id", "ContractId", "DateEnd", "DateStart" },
                values: new object[,]
                {
                    { new Guid("05936f8a-096c-45c7-946b-db111e625990"), new Guid("28c08503-c932-4160-aa41-a9cffa1fc630"), new DateTime(2024, 6, 1, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 5, 25, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("7381da40-883a-4a62-94ea-fa3165604ba6"), new Guid("b0c752f7-3a52-4f80-8b71-0b4eef05396b"), new DateTime(2024, 2, 20, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 15, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("800892cd-b5da-47f5-8d90-fc3d87ee8128"), new Guid("d6050cad-666d-43c0-9443-4ae7e7fd6a51"), new DateTime(2024, 4, 10, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 4, 1, 12, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "sheets",
                columns: new[] { "Id", "Amount", "ApprovedDate", "ContractId", "Date", "EmployeeId", "InvoiceId", "IsApproved", "ServiceId" },
                values: new object[,]
                {
                    { new Guid("35a58b18-844e-4081-a9c3-ab2683cbcbc4"), 15, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("d6050cad-666d-43c0-9443-4ae7e7fd6a51"), new DateTime(2024, 11, 17, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("10e63f7e-0bb8-46f7-a27a-411d9140cafc"), null, false, new Guid("764d9967-651d-4425-8237-8005b2f1ca32") },
                    { new Guid("baeb2b88-88cd-42fe-86d1-ed27435d9509"), 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("28c08503-c932-4160-aa41-a9cffa1fc630"), new DateTime(2024, 8, 13, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("10e63f7e-0bb8-46f7-a27a-411d9140cafc"), null, false, new Guid("e0521823-8640-45d1-9de8-0f2e01102b83") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_contracts_ClientId",
                table: "contracts",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_invoices_ContractId",
                table: "invoices",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_sheets_ContractId",
                table: "sheets",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_sheets_EmployeeId",
                table: "sheets",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_sheets_InvoiceId",
                table: "sheets",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_sheets_ServiceId",
                table: "sheets",
                column: "ServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Money");

            migrationBuilder.DropTable(
                name: "sheets");

            migrationBuilder.DropTable(
                name: "userroles");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "invoices");

            migrationBuilder.DropTable(
                name: "services");

            migrationBuilder.DropTable(
                name: "contracts");

            migrationBuilder.DropTable(
                name: "clients");
        }
    }
}