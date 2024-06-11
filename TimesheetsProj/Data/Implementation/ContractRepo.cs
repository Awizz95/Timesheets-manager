using Microsoft.EntityFrameworkCore;
using TimesheetsProj.Data.Ef;
using TimesheetsProj.Data.Interfaces;
using TimesheetsProj.Models;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Data.Implementation
{
    public class ContractRepo : IContractRepo
    {
        private readonly TimesheetDbContext _dbContext;

        public ContractRepo(TimesheetDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Contract?> Get(Guid id)
        {
            Contract? result = await _dbContext.Contracts.FindAsync(id);

            return result;
        }

        public async Task<IEnumerable<Contract>> GetAll()
        {
            List<Contract> result = await _dbContext.Contracts.ToListAsync();

            return result;
        }

        public async Task<IEnumerable<Contract>> GetAllByClient(Guid clientId)
        {
            List<Contract> contracts = await _dbContext.Contracts.Where(x => x.ClientId == clientId).ToListAsync();

            return contracts;
        }

        public async Task Create(Contract item)
        {
            await _dbContext.Contracts.AddAsync(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Contract contract)
        {
            await _dbContext.Contracts.Where(x => x.Id == contract.Id).ExecuteUpdateAsync(x => x
                .SetProperty(x => x.Title, contract.Title)
                .SetProperty(x => x.Description, contract.Description)
                .SetProperty(x => x.DateStart, contract.DateStart)
                .SetProperty(x => x.DateEnd, contract.DateEnd));

            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> CheckContractIsActive(Guid id)
        {
            Contract? contract = await Get(id);

            if (contract is null) throw new InvalidOperationException($"Контракт с id:{id} не найден!");

            DateTime now = DateTime.Now;
            bool isActive = now <= contract.DateEnd && now >= contract.DateStart;

            return isActive;
        }
    }
}
