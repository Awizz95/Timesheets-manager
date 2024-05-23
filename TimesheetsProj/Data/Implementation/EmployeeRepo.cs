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

        public async Task<int> Add(Employee item)
        {
            await _dbContext.Employees.AddAsync(item);
            int result = await _dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<Employee?> GetItem(Guid id)
        {
            var result = await _dbContext.Employees.FindAsync(id);
            return result;
        }

        public async Task<IEnumerable<Employee>> GetItems()
        {
            var result = await _dbContext.Employees.ToListAsync();

            return result;
        }

        public async Task<int> Update(Guid employeeId, Employee item)
        {
            _dbContext.Employees.Update(item);
            int result = await _dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<bool> CheckEmployeeIsDeleted(Guid id)
        {
            var employee = await _dbContext.Employees.Where(x => x.Id == id).SingleAsync();
            var status = employee.IsDeleted;

            return status;
        }

        public async Task<IEnumerable<Sheet>?> GetAllSheets(Guid id)
        {
            var employee = await GetItem(id);
            var sheets = employee.Sheets;

            return sheets;
        }
    }
}
