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
            await _contractRepo.Create(contract);

            return contract.Id;
        }

        public async Task<Contract> Get(Guid contractId)
        {
            Contract? contract = await _contractRepo.Get(contractId);

            if (contract is not null) return contract;

            throw new InvalidOperationException($"Контракт с id: {contractId} не найден!");
        }

        public async Task<IEnumerable<Contract>> GetAll()
        {
            IEnumerable<Contract> contracts = await _contractRepo.GetAll();

            if (!contracts.Any()) throw new InvalidOperationException("Список контрактов пустой!");

            return contracts;
        }

        public async Task Update(Guid contractId, ContractRequest request)
        {
            Contract contract = ContractMapper.ContractRequestToUpdateContract(contractId, request);
            await _contractRepo.Update(contract);
        }

        public async Task<bool> CheckContractIsActive(Guid id)
        {
            bool result;

            try
            {
                result = await _contractRepo.CheckContractIsActive(id);
            }
            catch(InvalidOperationException)
            {
                throw;
            }

            return result;
        }
    }
}
