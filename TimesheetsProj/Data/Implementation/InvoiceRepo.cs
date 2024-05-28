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

        public async Task<Invoice?> Get(Guid invoiceId)
        {
            Invoice? result = await _dbContext.Invoices.FindAsync(invoiceId);

            return result;
        }

        public async Task<IEnumerable<Invoice>?> GetAll()
        {
            List<Invoice> result = await _dbContext.Invoices.ToListAsync();

            return result;
        }

        public async Task Create(Invoice invoice)
        {
            await _dbContext.Invoices.AddAsync(invoice);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Invoice invoice)
        {
            await _dbContext.Invoices.Where(x => x.Id == invoice.Id).ExecuteUpdateAsync(x => x
               .SetProperty(x => x.ContractId, invoice.ContractId)
               .SetProperty(x => x.DateStart, invoice.DateStart)
               .SetProperty(x => x.DateEnd, invoice.DateEnd)
               .SetProperty(x => x.Sum, invoice.Sum)
               .SetProperty(x => x.Contract, invoice.Contract)
               .SetProperty(x => x.Sheets, invoice.Sheets));

            await _dbContext.SaveChangesAsync();
        }

        public async Task<Contract?> GetContract(Guid invoiceId)
        {
            Invoice? invoice = await Get(invoiceId);

            if (invoice is null) throw new InvalidOperationException($"Счета с id: {invoiceId} не существует");
                
            Contract? contract = invoice.Contract;

            return contract;
        }

        public async Task<IEnumerable<Sheet>> GetSheets(Guid invoiceId)
        {
            Invoice? invoice = await Get(invoiceId);

            if (invoice is null) throw new InvalidOperationException($"Счета с id: {invoiceId} не существует");

            List<Sheet> sheets = invoice.Sheets;

            return sheets;
        }
    }
}
