using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Data.Ef.Configurations
{
    public class ContractConfiguration : IEntityTypeConfiguration<Contract>
    {
        public static Contract[] GetInitialData()
        {
            Contract [] contracts = [
                new Contract {
                    Id = Guid.Parse("28c08503-c932-4160-aa41-a9cffa1fc630"),
                    Title = "Тестовый действующий контракт #1",
                    DateStart = new DateTime(2024,05,20,12,00,00,DateTimeKind.Utc),
                    DateEnd = new DateTime(2024, 12,31,23,59,00,DateTimeKind.Utc),
                    Description = "Описание контракта #1",
                    IsDeleted = false,
                    ClientId = Guid.Parse("d0d43c64-ae2b-4518-a41c-8d24031abcc5")
                    },
                new Contract {
                    Id = Guid.Parse("d6050cad-666d-43c0-9443-4ae7e7fd6a51"),
                    Title = "Тестовый действующий контракт #2",
                    DateStart = new DateTime(2024,03,24,23,00,00,DateTimeKind.Utc),
                    DateEnd = new DateTime(2025, 12,31,23,59,00,DateTimeKind.Utc),
                    Description = "Описание контракта #2",
                    IsDeleted = false,
                    ClientId = Guid.Parse("d0d43c64-ae2b-4518-a41c-8d24031abcc5")
                    },
                new Contract {
                    Id = Guid.Parse("b0c752f7-3a52-4f80-8b71-0b4eef05396b"),
                    Title = "Тестовый действующий контракт #3",
                    DateStart = new DateTime(2024,01,10,11,00,00,DateTimeKind.Utc),
                    DateEnd = new DateTime(2026, 05,31,23,00,00,DateTimeKind.Utc),
                    Description = "Описание контракта #3",
                    IsDeleted = false,
                    ClientId = Guid.Parse("d0d43c64-ae2b-4518-a41c-8d24031abcc5")
                    },
                new Contract {
                    Id = Guid.Parse("8ed5761c-858e-4106-a689-168c4e59c5d4"),
                    Title = "Тестовый истекший контракт #4",
                    DateStart = new DateTime(2024,02,10,11,00,00,DateTimeKind.Utc),
                    DateEnd = new DateTime(2024, 04,05,10,00,00,DateTimeKind.Utc),
                    Description = "Описание контракта #4",
                    IsDeleted = false,
                    ClientId = Guid.Parse("d0d43c64-ae2b-4518-a41c-8d24031abcc5")
                    }
                ];

            return contracts;
        }

        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.ToTable("contracts");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.HasData(GetInitialData());
        }
    }
}
