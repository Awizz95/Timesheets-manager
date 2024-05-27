using Microsoft.EntityFrameworkCore;
using TimesheetsProj.Data.Ef;
using TimesheetsProj.Data.Interfaces;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Data.Implementation
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly TimesheetDbContext _dbContext;

        public EmployeeRepo(TimesheetDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(User user)
        {
            Employee employee = new Employee
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Sheets = [],
                IsDeleted = false
            };

            await _dbContext.Employees.AddAsync(employee);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Employee?> Get(Guid id)
        {
            var result = await _dbContext.Employees.FindAsync(id);

            return result;
        }

        public async Task<IEnumerable<Employee>?> GetAll()
        {
            var result = await _dbContext.Employees.ToListAsync();

            return result;
        }

        public async Task Update(Employee employee)
        {
            await _dbContext.Employees.Where(x => x.Id == employee.Id).ExecuteUpdateAsync(x => x
.SetProperty(x => x.UserId, employee.UserId)
.SetProperty(x => x.Sheets, employee.Sheets)
.SetProperty(x => x.IsDeleted, employee.IsDeleted));

            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> CheckEmployeeIsDeleted(Guid id)
        {
            var employee = await _dbContext.Employees.Where(x => x.Id == id).SingleAsync();
            var status = employee.IsDeleted;

            return status;
        }

        public async Task<IEnumerable<Sheet>?> GetAllSheets(Guid id)
        {
            var employee = await Get(id);
            var sheets = employee.Sheets;

            return sheets;
        }
    }
}
