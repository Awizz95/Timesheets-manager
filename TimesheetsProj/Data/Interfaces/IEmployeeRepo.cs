using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Data.Interfaces
{
    public interface IEmployeeRepo : IRepoBase<Employee>
    {
        public Task<bool> CheckEmployeeIsDeleted(Guid id);
        public Task<IEnumerable<Sheet>?> GetAllSheets(Guid id);
    }
}
