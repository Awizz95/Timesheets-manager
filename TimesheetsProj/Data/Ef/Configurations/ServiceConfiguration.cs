using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Data.Ef.Configurations
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public static Service[] GetInitialData()
        {
            Service[] services = [
                new Service{
                    Id = Guid.Parse("e0521823-8640-45d1-9de8-0f2e01102b83"),
                    Name = "Тестовый сервис #1",
                    Cost = 5.435m
                },
                new Service{
                    Id = Guid.Parse("764d9967-651d-4425-8237-8005b2f1ca32"),
                    Name = "Тестовый сервис #2",
                    Cost = 10.877m
                },
                new Service{
                    Id = Guid.Parse("3647ae1a-818d-43db-9c5e-9027b0a78e33"),
                    Name = "Тестовый сервис #3",
                    Cost = 155.555m
                }];

            return services;
        }

        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.ToTable("services");
            builder.HasData(GetInitialData());
        }
    }
}
