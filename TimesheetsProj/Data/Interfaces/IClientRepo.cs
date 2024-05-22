using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Data.Interfaces
{
    public interface IClientRepo : IRepoBase<Client>
    {
        public Task<bool> CheckClientIsDeleted(Guid id);
    }
}
