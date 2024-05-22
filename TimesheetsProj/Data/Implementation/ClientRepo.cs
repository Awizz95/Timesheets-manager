using Microsoft.EntityFrameworkCore;
using TimesheetsProj.Data.Ef;
using TimesheetsProj.Data.Interfaces;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Data.Implementation
{
    public class ClientRepo : IClientRepo
    {
        private readonly TimesheetDbContext _dbContext;

        public ClientRepo(TimesheetDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Client item)
        {
            await _dbContext.Clients.AddAsync(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Client> GetItem(Guid id)
        {
            var result = await _dbContext.Clients.FindAsync(id);
            return result;
        }

        public async Task<IEnumerable<Client>> GetItems()
        {
            var result = await _dbContext.Clients.ToListAsync();

            return result;
        }

        public async Task Update(Client item)
        {
            _dbContext.Clients.Update(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> CheckClientIsDeleted(Guid id)
        {
            var client = await _dbContext.Clients.Where(x => x.Id == id).SingleAsync();
            var status = client.IsDeleted;

            return status;
        }

        public async Task<IEnumerable<Contract>?> GetAllContracts(Guid id)
        {
            var client = await GetItem(id);
            var contracts = client.Contracts;

            return contracts;
        }
    }
}
