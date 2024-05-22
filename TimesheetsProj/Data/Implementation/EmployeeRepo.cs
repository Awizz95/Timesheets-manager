using TimesheetsProj.Data.Interfaces;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Data.Implementation
{
    public class EmployeeRepo : IEmployeeRepo
    {
        public Task Add(Employee item)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> GetItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Employee>> GetItems()
        {
            throw new NotImplementedException();
        }

        public Task Update(Employee item)
        {
            throw new NotImplementedException();
        }
    }
}
