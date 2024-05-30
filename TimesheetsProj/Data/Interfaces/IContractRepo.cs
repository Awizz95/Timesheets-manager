using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Data.Interfaces
{
    public interface IContractRepo
    {
        Task Create(Contract item);
        Task<Contract?> Get(Guid id);
        Task<IEnumerable<Contract>> GetAll();
        Task Update(Contract item);
        Task<bool> CheckContractIsActive(Guid id);
    }
}
