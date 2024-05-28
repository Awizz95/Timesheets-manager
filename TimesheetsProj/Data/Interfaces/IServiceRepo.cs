using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Data.Interfaces
{
    public interface IServiceRepo
    {
        Task<IEnumerable<Service>> GetAll();
        Task<Service> Get(Guid id);
        Task Create(Service service);
        Task Update(Service service);
        Task<IEnumerable<Sheet>> GetSheets(Guid id);
        Task<bool> ServiceExists(Guid id);
    }
}
