using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Data.Ef.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public static Employee[] GetInitialData()
        {
            Employee[] employees = [
                new Employee{
                    Id = Guid.Parse("10e63f7e-0bb8-46f7-a27a-411d9140cafc"),
                    UserId = Guid.Parse("769c84d7-01bd-4e4f-aeac-ef4963e9bd84"),
                    IsDeleted = false
            }];

            return employees;
        }

        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("employees");
            builder.HasData(GetInitialData());
        }
    }
}
