using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Data.Interfaces
{
    public interface IServiceRepo
    {
        public Task<IEnumerable<Sheet>> GetSheets(Guid id);
    }
}
