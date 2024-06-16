using TimesheetsProj.Data.Interfaces;
using TimesheetsProj.Domain.Managers.Interfaces;
using TimesheetsProj.Infrastructure.Mappers;
using TimesheetsProj.Models;
using TimesheetsProj.Models.Dto.Requests;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Domain.Managers.Implementation
{
    public class ContractManager : IContractManager
    {
        private readonly IContractRepo _contractRepo;
        private readonly IUserRepo _userRepo;

        public ContractManager(IContractRepo contractRepo, IUserRepo userRepo)
        {
            _contractRepo = contractRepo;
            _userRepo = userRepo;
        }

        public async Task<Guid> Create(ContractRequest request)
        {
            Contract contract = ContractMapper.ContractRequestToContract(request);
            await _contractRepo.Create(contract);

            return contract.Id;
        }

        public async Task<Contract> Get(Guid contractId)
        {
            Contract? contract = await _contractRepo.Get(contractId) ?? throw new InvalidOperationException($"Объект с id: {contractId} не найден");

            return contract;
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

        public async Task<IEnumerable<Contract>> GetAllByClient(Guid clientId)
        {
            IEnumerable<Contract> contracts;

            try
            {
                User? user = await _userRepo.GetByUserId(clientId);

                if (user is null || user.Role != UserRoles.Client.ToString())
                    throw new InvalidOperationException($"Пользователя с id: {clientId} не существует или он является клиентом");

                contracts = await _contractRepo.GetAllByClient(clientId);
            }
            catch (InvalidOperationException)
            {
                throw;
            }

            return contracts;
        }
    }
}
