using Microsoft.EntityFrameworkCore;
using TimesheetsProj.Data.Ef;
using TimesheetsProj.Data.Interfaces;
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

        public async Task<IEnumerable<Contract>?> GetAll()
        {
            List<Contract> result = await _dbContext.Contracts.ToListAsync();

            return result;
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
            var contract = await _dbContext.Contracts.FindAsync(id);
            var now = DateTime.Now;
            var isActive = now <= contract?.DateEnd && now >= contract?.DateStart;

            return isActive;
        }
    }
}
