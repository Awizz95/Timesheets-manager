using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Data.Ef.Configurations
{
    public class SheetConfiguration : IEntityTypeConfiguration<Sheet>
    {
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
        }
    }
}
