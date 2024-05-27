using TimesheetsProj.Models.Dto.Requests;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Domain.Managers.Interfaces
{
    public interface ISheetManager
    {
        Task<Sheet?> Get(Guid id);
        Task<IEnumerable<Sheet>?> GetAll();
        Task<Guid> Create(SheetRequest sheet);
        Task Update(SheetRequest sheetRequest);
        Task Approve(Sheet sheet, DateTime approvedDate);
    }
}
