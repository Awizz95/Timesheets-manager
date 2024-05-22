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

        public async Task Add(Contract item)
        {
            await _dbContext.Contracts.AddAsync(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Contract item)
        {
            _dbContext.Contracts.Update(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool?> CheckContractIsActive(Guid id)
        {
            var contract = await _dbContext.Contracts.FindAsync(id);
            var now = DateTime.Now;
            var isActive = now <= contract?.DateEnd && now >= contract?.DateStart;

            return isActive;
        }
    }
}
