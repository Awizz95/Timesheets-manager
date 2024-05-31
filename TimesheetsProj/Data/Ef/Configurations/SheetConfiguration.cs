using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Data.Ef.Configurations
{
    public class SheetConfiguration : IEntityTypeConfiguration<Sheet>
    {
        public static Sheet[] GetInitialData()
        {
            Sheet[] sheets = [
                new Sheet{
                    Id = Guid.Parse("baeb2b88-88cd-42fe-86d1-ed27435d9509"),
                    Date = new DateTime(2024,08,13,12,00,00,DateTimeKind.Utc),
                    EmployeeId = Guid.Parse("10e63f7e-0bb8-46f7-a27a-411d9140cafc"),
                    ContractId = Guid.Parse("28c08503-c932-4160-aa41-a9cffa1fc630"),
                    ServiceId = Guid.Parse("e0521823-8640-45d1-9de8-0f2e01102b83"),
                    Amount = 5,
                    IsApproved = false
                },
                new Sheet{
                    Id = Guid.Parse("35a58b18-844e-4081-a9c3-ab2683cbcbc4"),
                    Date = new DateTime(2024,11,17,12,00,00,DateTimeKind.Utc),
                    EmployeeId = Guid.Parse("10e63f7e-0bb8-46f7-a27a-411d9140cafc"),
                    ContractId = Guid.Parse("d6050cad-666d-43c0-9443-4ae7e7fd6a51"),
                    ServiceId = Guid.Parse("764d9967-651d-4425-8237-8005b2f1ca32"),
                    Amount = 15,
                    IsApproved = false
                }];

            return sheets;
        }

        public void Configure(EntityTypeBuilder<Sheet> builder)
        {
            builder.ToTable("sheets");

            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .HasColumnName("Id");

            builder
                .HasOne(sheet => sheet.Invoice)
                .WithMany(invoice => invoice.Sheets)
                .HasForeignKey("InvoiceId");

            builder
                .HasOne(sheet => sheet.Contract)
                .WithMany(contract => contract.Sheets)
                .HasForeignKey("ContractId")
                .IsRequired();

            builder
                .HasOne(sheet => sheet.Service)
                .WithMany(service => service.Sheets)
                .HasForeignKey("ServiceId")
                .IsRequired();

            builder
                .HasOne(sheet => sheet.Employee)
                .WithMany(employee => employee.Sheets)
                .HasForeignKey("EmployeeId")
                .IsRequired();

            builder.HasData(GetInitialData());
        }
    }
}
