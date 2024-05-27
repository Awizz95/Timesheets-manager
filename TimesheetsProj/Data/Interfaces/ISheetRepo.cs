using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Data.Interfaces
{
    public interface ISheetRepo
    {
        Task<Sheet?> Get(Guid id);
        Task<IEnumerable<Sheet>?> GetAll();
        Task Create(Sheet item);
        Task Update(Sheet item);
        Task<IEnumerable<Sheet>> GetItemsForInvoice(Guid contractId, DateTime dateStart, DateTime dateEnd);
    }
}
