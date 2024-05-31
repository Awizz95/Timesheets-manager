using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Data.Ef.Configurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public static Invoice[] GetInitialData()
        {
            //Счет к контракту #1
            Invoice[] invoices = [
                new Invoice{
                    Id = Guid.Parse("05936f8a-096c-45c7-946b-db111e625990"),
                    ContractId = Guid.Parse("28c08503-c932-4160-aa41-a9cffa1fc630"),
                    DateStart = new DateTime(2024,05,25,12,00,00,DateTimeKind.Utc),
                    DateEnd = new DateTime(2024,06,01,12,00,00,DateTimeKind.Utc)
                },
                //Счет к контракту #2
                new Invoice{
                    Id = Guid.Parse("800892cd-b5da-47f5-8d90-fc3d87ee8128"),
                    ContractId = Guid.Parse("d6050cad-666d-43c0-9443-4ae7e7fd6a51"),
                    DateStart = new DateTime(2024,04,01,12,00,00,DateTimeKind.Utc),
                    DateEnd = new DateTime(2024,04,10,12,00,00,DateTimeKind.Utc)
                },
                //Счет к контракту #3
                new Invoice{
                    Id = Guid.Parse("7381da40-883a-4a62-94ea-fa3165604ba6"),
                    ContractId = Guid.Parse("b0c752f7-3a52-4f80-8b71-0b4eef05396b"),
                    DateStart = new DateTime(2024,01,15,12,00,00,DateTimeKind.Utc),
                    DateEnd = new DateTime(2024,02,20,12,00,00,DateTimeKind.Utc)
                }];

            return invoices;
        }

        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("invoices");
            builder.HasData(GetInitialData());
        }
    }
}
