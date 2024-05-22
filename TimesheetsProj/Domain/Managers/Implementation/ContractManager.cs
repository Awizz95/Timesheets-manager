using TimesheetsProj.Data.Interfaces;
using TimesheetsProj.Domain.Managers.Interfaces;
using TimesheetsProj.Models.Dto.Requests;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Domain.Managers.Implementation
{
    public class ContractManager : IContractManager
    {
        private readonly IContractRepo _contractRepo;

        public ContractManager(IContractRepo contractRepo)
        {
            _contractRepo = contractRepo;
        }

        public Task<Guid> Create(ContractRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Contract> GetItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Contract>> GetItems()
        {
            throw new NotImplementedException();
        }

        public Task Update(Guid id, ContractRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<bool?> CheckContractIsActive(Guid id)
        {
            return await _contractRepo.CheckContractIsActive(id);
        }
    }
}
