using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Data.Interfaces
{
    public interface IInvoiceRepo
    {
        Task<Invoice?> Get(Guid invoiceId);
        Task<IEnumerable<Invoice>?> GetAll();
        Task Create(Invoice invoice);
        Task Update(Invoice invoice);
        Task<Contract?> GetContract(Guid invoiceId);
        Task<IEnumerable<Sheet>> GetSheets(Guid invoiceId);
    }
}
