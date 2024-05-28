using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;
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

        public async Task<Service> Get(Guid id)
        {
            Service? result = await _dbContext.Services.FindAsync(id);

            if(result is not null) return result;

            throw new InvalidOperationException("Данная услуга не найдена!");
        }

        public async Task<IEnumerable<Service>> GetAll()
        {
            List<Service> result = await _dbContext.Services.ToListAsync();

            return result;
        }

        public async Task Create(Service service)
        {
            await _dbContext.Services.AddAsync(service);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Service service)
        {
            await _dbContext.Services.Where(x => x.Id == service.Id).ExecuteUpdateAsync(x => x
    .SetProperty(x => x.Name, service.Name)
    .SetProperty(x => x.Sheets, service.Sheets));

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Sheet>> GetSheets(Guid id)
        {
            var service = await _dbContext.Services.Where(x => x.Id == id).SingleAsync();
            var sheets = service.Sheets;
            return sheets;
        }

        public async Task<bool> ServiceExists(Guid id)
        {
            bool result = await _dbContext.Services.AnyAsync(e => e.Id == id);

            return result;
        }
    }
}
