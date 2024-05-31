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
                    Username = "Max",
                    PasswordHash =
                        [94, 119, 237, 33, 80, 41, 39, 154, 45, 0, 255, 133, 53, 50, 146, 176, 48, 208, 107, 156], //111111
                    Role = "User"
                },

                new User{
                    Id = Guid.Parse("1744b83d-059e-4973-9c4f-0781c03f1079"),
                    Username = "Andrey",
                    PasswordHash =
                        [106,110,54,11,228,45,74,106,2,195,249,195,183,129,135,24,46,159,106,145], //222222
                    Role = "Admin"
                },
                new User{
                    Id = Guid.Parse("769c84d7-01bd-4e4f-aeac-ef4963e9bd84"),
                    Username = "Alex",
                    PasswordHash =
                        [103,241,94,178,185,71,163,213,221,106,1,99,200,0,142,40,143,26,239,114], //333333
                    Role = "Employee"
                },
                new User{
                    Id = Guid.Parse("d0d43c64-ae2b-4518-a41c-8d24031abcc5"),
                    Username = "Mark",
                    PasswordHash =
                        [172,159,122,37,13,70,11,221,231,73,199,12,190,87,38,231,139,172,169,221], //444444
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
