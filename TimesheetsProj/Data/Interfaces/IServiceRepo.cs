using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Data.Interfaces
{
    public interface IServiceRepo : IRepoBase<Service>
    {
        public Task<IEnumerable<Sheet>> GetSheets(Guid id);
    }
}
