using TimesheetsProj.Models.Dto.Requests;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Domain.Managers.Interfaces
{
    public interface IContractManager
    {
        Task<Contract> Get(Guid id);
        Task<IEnumerable<Contract>> GetAll();
        Task<Guid> Create(ContractRequest request);
        Task Update(Guid contractId, ContractRequest request);
        Task<bool> CheckContractIsActive(Guid id);
    }
}
