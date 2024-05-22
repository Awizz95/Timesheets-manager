using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Data.Interfaces
{
    public interface ISheetRepo : IRepoBase<Sheet>
    {
        Task<IEnumerable<Sheet>> GetItemsForInvoice(Guid contractId, DateTime dateStart, DateTime dateEnd);
    }
}
