using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Data.Interfaces
{
    public interface ISheetRepo
    {
        Task<Sheet?> Get(Guid id);
        Task<IEnumerable<Sheet>> GetAll();
        Task Create(Sheet item);
        Task Update(Sheet item);
        Task<IEnumerable<Sheet>> GetSheetsForInvoice(Guid invoiceId);
        Task IncludeInvoice(Guid sheetId, Guid invoiceId);
        Task<IEnumerable<Sheet>> GetAllByEmployee(Guid employeeId);
    }
}
