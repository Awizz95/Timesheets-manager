using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Data.Ef.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public static User[] GetInitialData()
        {
            User[] users = [
                new User{
                    Id = Guid.Parse("109f4988-c1cb-4845-b6c5-5a824cbcd338"),
                    Email = "max@gmail.com",
                    PasswordHash =
                        [197,136,170,49,232,10,104,164,82,104,225,219,230,163,168,41,139,234,216,206], //111111aA$
                    Role = "User"
                },

                new User{
                    Id = Guid.Parse("1744b83d-059e-4973-9c4f-0781c03f1079"),
                    Email = "andrey@gmail.com",
                    PasswordHash =
                        [139,45,244,222,249,92,169,8,97,182,206,249,49,72,88,212,220,168,14,205], //222222aA$
                    Role = "Admin"
                },
                new User{
                    Id = Guid.Parse("769c84d7-01bd-4e4f-aeac-ef4963e9bd84"),
                    Email = "alex@gmail.com",
                    PasswordHash =
                        [46,107,131,170,249,242,215,204,255,117,47,200,34,193,4,93,158,103,104,121], //333333aA$
                    Role = "Employee"
                },
                new User{
                    Id = Guid.Parse("d0d43c64-ae2b-4518-a41c-8d24031abcc5"),
                    Email = "mark@gmail.com",
                    PasswordHash =
                        [244,243,48,111,74,177,33,29,12,98,129,216,154,10,102,112,172,146,98,47], //444444aA$
                    Role = "Client"
                }];

            return users;
        }

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            builder.HasData(GetInitialData());
        }
    }
}
