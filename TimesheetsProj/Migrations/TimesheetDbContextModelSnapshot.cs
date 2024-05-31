﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TimesheetsProj.Data.Ef;

#nullable disable

namespace TimesheetsProj.Migrations
{
    [DbContext(typeof(TimesheetDbContext))]
    partial class TimesheetDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TimesheetsProj.Domain.ValueObjects.Money", b =>
                {
                    b.ToTable("Money");
                });

            modelBuilder.Entity("TimesheetsProj.Models.Entities.Client", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("clients", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("3287389a-acf3-4d19-b7ee-b19a89396b23"),
                            IsDeleted = false,
                            UserId = new Guid("d0d43c64-ae2b-4518-a41c-8d24031abcc5")
                        });
                });

            modelBuilder.Entity("TimesheetsProj.Models.Entities.Contract", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ClientId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("contracts", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("28c08503-c932-4160-aa41-a9cffa1fc630"),
                            DateEnd = new DateTime(2024, 12, 31, 23, 59, 0, 0, DateTimeKind.Utc),
                            DateStart = new DateTime(2024, 5, 20, 12, 0, 0, 0, DateTimeKind.Utc),
                            Description = "Описание контракта #1",
                            IsDeleted = false,
                            Title = "Тестовый действующий контракт #1"
                        },
                        new
                        {
                            Id = new Guid("d6050cad-666d-43c0-9443-4ae7e7fd6a51"),
                            DateEnd = new DateTime(2025, 12, 31, 23, 59, 0, 0, DateTimeKind.Utc),
                            DateStart = new DateTime(2024, 3, 24, 23, 0, 0, 0, DateTimeKind.Utc),
                            Description = "Описание контракта #2",
                            IsDeleted = false,
                            Title = "Тестовый действующий контракт #2"
                        },
                        new
                        {
                            Id = new Guid("b0c752f7-3a52-4f80-8b71-0b4eef05396b"),
                            DateEnd = new DateTime(2026, 5, 31, 23, 0, 0, 0, DateTimeKind.Utc),
                            DateStart = new DateTime(2024, 1, 10, 11, 0, 0, 0, DateTimeKind.Utc),
                            Description = "Описание контракта #3",
                            IsDeleted = false,
                            Title = "Тестовый действующий контракт #3"
                        },
                        new
                        {
                            Id = new Guid("8ed5761c-858e-4106-a689-168c4e59c5d4"),
                            DateEnd = new DateTime(2024, 4, 5, 10, 0, 0, 0, DateTimeKind.Utc),
                            DateStart = new DateTime(2024, 2, 10, 11, 0, 0, 0, DateTimeKind.Utc),
                            Description = "Описание контракта #4",
                            IsDeleted = false,
                            Title = "Тестовый истекший контракт #4"
                        });
                });

            modelBuilder.Entity("TimesheetsProj.Models.Entities.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("employees", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("10e63f7e-0bb8-46f7-a27a-411d9140cafc"),
                            IsDeleted = false,
                            UserId = new Guid("769c84d7-01bd-4e4f-aeac-ef4963e9bd84")
                        });
                });

            modelBuilder.Entity("TimesheetsProj.Models.Entities.Invoice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ContractId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ContractId");

                    b.ToTable("invoices", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("05936f8a-096c-45c7-946b-db111e625990"),
                            ContractId = new Guid("28c08503-c932-4160-aa41-a9cffa1fc630"),
                            DateEnd = new DateTime(2024, 6, 1, 12, 0, 0, 0, DateTimeKind.Utc),
                            DateStart = new DateTime(2024, 5, 25, 12, 0, 0, 0, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = new Guid("800892cd-b5da-47f5-8d90-fc3d87ee8128"),
                            ContractId = new Guid("d6050cad-666d-43c0-9443-4ae7e7fd6a51"),
                            DateEnd = new DateTime(2024, 4, 10, 12, 0, 0, 0, DateTimeKind.Utc),
                            DateStart = new DateTime(2024, 4, 1, 12, 0, 0, 0, DateTimeKind.Utc)
                        },
                        new
                        {
                            Id = new Guid("7381da40-883a-4a62-94ea-fa3165604ba6"),
                            ContractId = new Guid("b0c752f7-3a52-4f80-8b71-0b4eef05396b"),
                            DateEnd = new DateTime(2024, 2, 20, 12, 0, 0, 0, DateTimeKind.Utc),
                            DateStart = new DateTime(2024, 1, 15, 12, 0, 0, 0, DateTimeKind.Utc)
                        });
                });

            modelBuilder.Entity("TimesheetsProj.Models.Entities.Service", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Cost")
                        .HasColumnType("numeric");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("services", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("e0521823-8640-45d1-9de8-0f2e01102b83"),
                            Cost = 5.435m,
                            Name = "Тестовый сервис #1"
                        },
                        new
                        {
                            Id = new Guid("764d9967-651d-4425-8237-8005b2f1ca32"),
                            Cost = 10.877m,
                            Name = "Тестовый сервис #2"
                        },
                        new
                        {
                            Id = new Guid("3647ae1a-818d-43db-9c5e-9027b0a78e33"),
                            Cost = 155.555m,
                            Name = "Тестовый сервис #3"
                        });
                });

            modelBuilder.Entity("TimesheetsProj.Models.Entities.Sheet", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ApprovedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ContractId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("InvoiceId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("boolean");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ContractId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("ServiceId");

                    b.ToTable("sheets", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("baeb2b88-88cd-42fe-86d1-ed27435d9509"),
                            Amount = 5,
                            ApprovedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ContractId = new Guid("28c08503-c932-4160-aa41-a9cffa1fc630"),
                            Date = new DateTime(2024, 8, 13, 12, 0, 0, 0, DateTimeKind.Utc),
                            EmployeeId = new Guid("10e63f7e-0bb8-46f7-a27a-411d9140cafc"),
                            IsApproved = false,
                            ServiceId = new Guid("e0521823-8640-45d1-9de8-0f2e01102b83")
                        },
                        new
                        {
                            Id = new Guid("35a58b18-844e-4081-a9c3-ab2683cbcbc4"),
                            Amount = 15,
                            ApprovedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ContractId = new Guid("d6050cad-666d-43c0-9443-4ae7e7fd6a51"),
                            Date = new DateTime(2024, 11, 17, 12, 0, 0, 0, DateTimeKind.Utc),
                            EmployeeId = new Guid("10e63f7e-0bb8-46f7-a27a-411d9140cafc"),
                            IsApproved = false,
                            ServiceId = new Guid("764d9967-651d-4425-8237-8005b2f1ca32")
                        });
                });

            modelBuilder.Entity("TimesheetsProj.Models.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("109f4988-c1cb-4845-b6c5-5a824cbcd338"),
                            PasswordHash = new byte[] { 94, 119, 237, 33, 80, 41, 39, 154, 45, 0, 255, 133, 53, 50, 146, 176, 48, 208, 107, 156 },
                            Role = "User",
                            Username = "Max"
                        },
                        new
                        {
                            Id = new Guid("1744b83d-059e-4973-9c4f-0781c03f1079"),
                            PasswordHash = new byte[] { 106, 110, 54, 11, 228, 45, 74, 106, 2, 195, 249, 195, 183, 129, 135, 24, 46, 159, 106, 145 },
                            Role = "Admin",
                            Username = "Andrey"
                        },
                        new
                        {
                            Id = new Guid("769c84d7-01bd-4e4f-aeac-ef4963e9bd84"),
                            PasswordHash = new byte[] { 103, 241, 94, 178, 185, 71, 163, 213, 221, 106, 1, 99, 200, 0, 142, 40, 143, 26, 239, 114 },
                            Role = "Employee",
                            Username = "Alex"
                        },
                        new
                        {
                            Id = new Guid("d0d43c64-ae2b-4518-a41c-8d24031abcc5"),
                            PasswordHash = new byte[] { 172, 159, 122, 37, 13, 70, 11, 221, 231, 73, 199, 12, 190, 87, 38, 231, 139, 172, 169, 221 },
                            Role = "Client",
                            Username = "Mark"
                        });
                });

            modelBuilder.Entity("TimesheetsProj.Models.Entities.UserRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("userroles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("c3382a38-019c-4a62-bfbd-cba4c6b8e229"),
                            Name = "Admin"
                        },
                        new
                        {
                            Id = new Guid("92287388-b5a8-48af-8497-a137dd47ac58"),
                            Name = "User"
                        },
                        new
                        {
                            Id = new Guid("75016735-84f3-46f7-a2f2-fd55815f09d2"),
                            Name = "Client"
                        },
                        new
                        {
                            Id = new Guid("f9e490fc-c09e-459c-bae3-194d64df2015"),
                            Name = "Employee"
                        });
                });

            modelBuilder.Entity("TimesheetsProj.Models.Entities.Contract", b =>
                {
                    b.HasOne("TimesheetsProj.Models.Entities.Client", null)
                        .WithMany("Contracts")
                        .HasForeignKey("ClientId");
                });

            modelBuilder.Entity("TimesheetsProj.Models.Entities.Invoice", b =>
                {
                    b.HasOne("TimesheetsProj.Models.Entities.Contract", "Contract")
                        .WithMany()
                        .HasForeignKey("ContractId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contract");
                });

            modelBuilder.Entity("TimesheetsProj.Models.Entities.Sheet", b =>
                {
                    b.HasOne("TimesheetsProj.Models.Entities.Contract", "Contract")
                        .WithMany("Sheets")
                        .HasForeignKey("ContractId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TimesheetsProj.Models.Entities.Employee", "Employee")
                        .WithMany("Sheets")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TimesheetsProj.Models.Entities.Invoice", "Invoice")
                        .WithMany("Sheets")
                        .HasForeignKey("InvoiceId");

                    b.HasOne("TimesheetsProj.Models.Entities.Service", "Service")
                        .WithMany("Sheets")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contract");

                    b.Navigation("Employee");

                    b.Navigation("Invoice");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("TimesheetsProj.Models.Entities.Client", b =>
                {
                    b.Navigation("Contracts");
                });

            modelBuilder.Entity("TimesheetsProj.Models.Entities.Contract", b =>
                {
                    b.Navigation("Sheets");
                });

            modelBuilder.Entity("TimesheetsProj.Models.Entities.Employee", b =>
                {
                    b.Navigation("Sheets");
                });

            modelBuilder.Entity("TimesheetsProj.Models.Entities.Invoice", b =>
                {
                    b.Navigation("Sheets");
                });

            modelBuilder.Entity("TimesheetsProj.Models.Entities.Service", b =>
                {
                    b.Navigation("Sheets");
                });
#pragma warning restore 612, 618
        }
    }
}
