using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Data.Interfaces
{
    public interface IClientRepo 
    {
        Task Create(User user);
        Task<IEnumerable<Client>?> GetAll();
        Task<Client?> Get(Guid id);
        Task Update(Client item);
        Task<IEnumerable<Contract>?> GetAllContracts(Guid id);
        Task<bool> CheckClientIsDeleted(Guid id);
    }
}
