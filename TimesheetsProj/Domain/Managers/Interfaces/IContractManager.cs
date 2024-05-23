using TimesheetsProj.Models.Dto.Requests;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Domain.Managers.Interfaces
{
    public interface IContractManager
    {
        Task<Contract> GetItem(Guid id);
        Task<IEnumerable<Contract>> GetItems();
        Task<Guid> Create(ContractRequest request);
        Task Update(Guid id, ContractRequest request);
        Task<bool> CheckContractIsActive(Guid id);
    }
}
