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

        public async Task<Contract> GetItem(Guid id)
        {
            var result = await _dbContext.Contracts.FindAsync(id);
            return result;
        }

        public async Task<IEnumerable<Contract>> GetItems()
        {
            var result = await _dbContext.Contracts.ToListAsync();

            return result;
        }

        public async Task<int> Add(Contract item)
        {
            await _dbContext.Contracts.AddAsync(item);
            int result = await _dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> Update(Guid contractId, Contract item)
        {
            await _dbContext.Contracts.Where(x => x.Id == contractId).ExecuteUpdateAsync(x => x
                .SetProperty(x => x.Title, item.Title)
                .SetProperty(x => x.Description, item.Description)
                .SetProperty(x => x.DateStart, item.DateStart)
                .SetProperty(x => x.DateEnd, item.DateEnd));

            int result = await _dbContext.SaveChangesAsync();

            return result;
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
