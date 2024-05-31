using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Data.Ef.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public static Client[] GetInitialData()
        {
            Client[] clients = [
                new Client{
                    Id = Guid.Parse("3287389a-acf3-4d19-b7ee-b19a89396b23"),
                    UserId = Guid.Parse("d0d43c64-ae2b-4518-a41c-8d24031abcc5"),
                    IsDeleted = false
            }];

            return clients;
        }

        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("clients");
            builder.HasData(GetInitialData());
        }
    }
}
