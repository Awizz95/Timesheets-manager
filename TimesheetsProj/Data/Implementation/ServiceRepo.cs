using Microsoft.EntityFrameworkCore;
using TimesheetsProj.Data.Ef;
using TimesheetsProj.Data.Interfaces;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Data.Implementation
{
    public class ServiceRepo : IServiceRepo
    {
        private readonly TimesheetDbContext _dbContext;

        public ServiceRepo(TimesheetDbContext context)
        {
            _dbContext = context;
        }

        public async Task<Service> GetItem(Guid id)
        {
            var result = await _dbContext.Services.FindAsync(id);

            return result;
        }

        public async Task<IEnumerable<Service>> GetItems()
        {
            var result = await _dbContext.Services.ToListAsync();

            return result;
        }

        public async Task Add(Service item)
        {
            await _dbContext.Services.AddAsync(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Service item)
        {
            _dbContext.Services.Update(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Sheet>> GetSheets(Guid id)
        {
            var service = await _dbContext.Services.Where(x => x.Id == id).SingleAsync();
            var sheets = service.Sheets;
            return sheets;
        }
    }
}
