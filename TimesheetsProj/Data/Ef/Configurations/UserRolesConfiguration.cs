using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Data.Ef.Configurations
{
    public class UserRolesConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public static UserRole[] GetInitialData()
        {
            UserRole[] userRoles = [
                new UserRole{
                    Id = Guid.Parse("c3382a38-019c-4a62-bfbd-cba4c6b8e229"),
                    Name = "Admin"
                },
                new UserRole{
                    Id = Guid.Parse("92287388-b5a8-48af-8497-a137dd47ac58"),
                    Name = "User"
                },
                new UserRole{
                    Id = Guid.Parse("75016735-84f3-46f7-a2f2-fd55815f09d2"),
                    Name = "Client"
                },
                new UserRole{
                    Id = Guid.Parse("f9e490fc-c09e-459c-bae3-194d64df2015"),
                    Name = "Employee"
                }];

            return userRoles;
        }

        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("userroles");
            builder.HasData(GetInitialData());
        }
    }
}
