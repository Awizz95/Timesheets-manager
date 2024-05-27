using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Data.Interfaces
{
    public interface IEmployeeRepo
    {
        Task Create(User user);
        Task<Employee?> Get(Guid id);
        Task<IEnumerable<Employee>?> GetAll();
        Task Update(Employee employee);
        public Task<bool> CheckEmployeeIsDeleted(Guid id);
        public Task<IEnumerable<Sheet>?> GetAllSheets(Guid id);
    }
}
