using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;
using TimesheetsProj.Data.Ef;
using TimesheetsProj.Data.Interfaces;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Data.Implementation
{
    public class SheetRepo : ISheetRepo
    {
        private readonly TimesheetDbContext _dbContext;

        public SheetRepo(TimesheetDbContext context)
        {
            _dbContext = context;
        }

        public async Task<Sheet?> Get(Guid id)
        {
            Sheet? result = await _dbContext.Sheets.FindAsync(id);

            return result;
        }

        public async Task<IEnumerable<Sheet>> GetAll()
        {
            List<Sheet> result = await _dbContext.Sheets.ToListAsync();

            return result;
        }

        public async Task Create(Sheet sheet)
        {
            await _dbContext.Sheets.AddAsync(sheet);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Sheet sheet)
        {
            await _dbContext.Sheets.Where(x => x.Id == sheet.Id).ExecuteUpdateAsync(x => x
                .SetProperty(x => x.ContractId, sheet.ContractId)
                .SetProperty(x => x.InvoiceId, sheet.InvoiceId)
                .SetProperty(x => x.Amount, sheet.Amount)
                .SetProperty(x => x.ServiceId, sheet.ServiceId)
                .SetProperty(x => x.IsApproved, sheet.IsApproved)
                .SetProperty(x => x.ApprovedDate, sheet.ApprovedDate)
                .SetProperty(x => x.Date, sheet.Date)
                .SetProperty(x => x.EmployeeId, sheet.EmployeeId));

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Sheet>> GetSheetsForInvoice(Guid invoiceId)
        {
            IEnumerable<Sheet> sheets = await _dbContext.Sheets
                .Where(x => x.InvoiceId == invoiceId)
                .ToListAsync();

            return sheets;
        }

        public async Task IncludeInvoice(Guid sheetId, Guid invoiceId)
        {
            await _dbContext.Sheets.Where(x => x.Id == sheetId).ExecuteUpdateAsync(x => x
            .SetProperty(x => x.InvoiceId, invoiceId));

            await _dbContext.SaveChangesAsync();
        }
    }
}
