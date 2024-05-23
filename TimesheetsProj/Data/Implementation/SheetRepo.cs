using Microsoft.EntityFrameworkCore;
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

        public async Task<Sheet> GetItem(Guid id)
        {
            var result = await _dbContext.Sheets.FindAsync(id);

            return result;
        }

        public async Task<IEnumerable<Sheet>> GetItems()
        {
            var result = await _dbContext.Sheets.ToListAsync();

            return result;
        }

        public async Task<int> Add(Sheet item)
        {
            await _dbContext.Sheets.AddAsync(item);
            int result = await _dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> Update(Guid sheetId, Sheet item)
        {
            _dbContext.Sheets.Update(item);
            int result= await _dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<IEnumerable<Sheet>> GetItemsForInvoice(Guid contractId, DateTime dateStart, DateTime dateEnd)
        {
            var sheets = await _dbContext.Sheets
                .Where(x => x.ContractId == contractId)
                .Where(x => x.Date >= dateStart && x.Date <= dateEnd)
                .ToListAsync();

            return sheets;
        }
    }
}
