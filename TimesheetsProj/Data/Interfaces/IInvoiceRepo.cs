using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Data.Interfaces
{
    public interface IInvoiceRepo
    {
        Task<Invoice> Get(Guid id);
        Task<IEnumerable<Invoice>> GetAll();
        Task Create(Invoice item);
        Task Update(Invoice item);
        public Task<Contract> GetContract(Guid id);
        public Task<IEnumerable<Sheet>> GetSheets(Guid id);
    }
}
