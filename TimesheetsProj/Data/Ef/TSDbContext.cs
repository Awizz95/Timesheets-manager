using Microsoft.EntityFrameworkCore;
using TimesheetsProj.Data.Ef.Configurations;
using TimesheetsProj.Domain.ValueObjects;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Data.Ef
{
    public class TimesheetDbContext : DbContext
    {
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Sheet> Sheets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public TimesheetDbContext(DbContextOptions<TimesheetDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContractConfiguration());
            modelBuilder.ApplyConfiguration(new InvoiceConfiguration());
            modelBuilder.ApplyConfiguration(new ServiceConfiguration());
            modelBuilder.ApplyConfiguration(new SheetConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserRolesConfiguration());
            modelBuilder.Entity<Money>().HasNoKey();
        }
    }
}
