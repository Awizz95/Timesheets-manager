using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Data.Interfaces
{
    public interface IInvoiceRepo : IRepoBase<Invoice>
    {
        public Task<Contract> GetContract(Guid id);
        public Task<IEnumerable<Sheet>> GetSheets(Guid id);
    }
}
