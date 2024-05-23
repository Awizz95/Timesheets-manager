using Microsoft.EntityFrameworkCore;
using TimesheetsProj.Data.Ef;
using TimesheetsProj.Data.Interfaces;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Data.Implementation
{
    public class InvoiceRepo : IInvoiceRepo
    {
        private readonly TimesheetDbContext _dbContext;

        public InvoiceRepo(TimesheetDbContext context)
        {
            _dbContext = context;
        }

        public async Task<Invoice> GetItem(Guid id)
        {
            var result = await _dbContext.Invoices.FindAsync(id);

            return result;
        }

        public async Task<IEnumerable<Invoice>> GetItems()
        {
            var result = await _dbContext.Invoices.ToListAsync();

            return result;
        }

        public async Task<int> Add(Invoice item)
        {
            await _dbContext.Invoices.AddAsync(item);
            int result = await _dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> Update(Guid invoiceId, Invoice item)
        {
            _dbContext.Invoices.Update(item);
            int result = await _dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<Contract> GetContract(Guid id)
        {
            var service = await _dbContext.Invoices.FindAsync(id);
            var contract = service.Contract;
            return contract;
        }

        public async Task<IEnumerable<Sheet>> GetSheets(Guid id)
        {
            var service = await _dbContext.Invoices.FindAsync(id);
            var sheets = service.Sheets;
            return sheets;
        }
    }
}
