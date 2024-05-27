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

        public async Task Create(User user)
        {
            Client client = new Client
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Contracts = [],
                IsDeleted = false
            };

            await _dbContext.Clients.AddAsync(client);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Client?> Get(Guid id)
        {
            var result = await _dbContext.Clients.FindAsync(id);

            return result;
        }

        public async Task<IEnumerable<Client>?> GetAll()
        {
            var result = await _dbContext.Clients.ToListAsync();

            return result;
        }

        public async Task Update(Client client)
        {
            await _dbContext.Clients.Where(x => x.Id == client.Id).ExecuteUpdateAsync(x => x
    .SetProperty(x => x.UserId, client.UserId)
    .SetProperty(x => x.Contracts, client.Contracts)
    .SetProperty(x => x.IsDeleted, client.IsDeleted));

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
            var client = await Get(id);
            var contracts = client.Contracts;

            return contracts;
        }
    }
}
