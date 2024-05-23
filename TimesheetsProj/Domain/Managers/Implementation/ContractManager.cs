using TimesheetsProj.Data.Interfaces;
using TimesheetsProj.Domain.Managers.Interfaces;
using TimesheetsProj.Domain.Mapper;
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

        public async Task<Guid> Create(ContractRequest request)
        {
            Contract contract = ContractMapper.ContractRequestToContract(request);
            await _contractRepo.Add(contract);

            return contract.Id;
        }

        public async Task<Contract> GetItem(Guid contractId)
        {
            var contract = await _contractRepo.GetItem(contractId);
            return contract;
        }

        public async Task<IEnumerable<Contract>> GetItems()
        {
            var contracts = await _contractRepo.GetItems();
            return contracts;
        }

        public async Task Update(Guid contractId, ContractRequest request)
        {
            var contract = ContractMapper.ContractRequestToContract(request);
            await _contractRepo.Update(contractId, contract);
        }

        public async Task<bool> CheckContractIsActive(Guid id)
        {
            return await _contractRepo.CheckContractIsActive(id);
        }
    }
}
