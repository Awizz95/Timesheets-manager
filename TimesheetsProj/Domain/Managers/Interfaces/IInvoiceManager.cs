using TimesheetsProj.Domain.ValueObjects;
using TimesheetsProj.Models.Dto.Requests;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Domain.Managers.Interfaces
{
    public interface IInvoiceManager
    {
        Task<Invoice> Get(Guid invoiceId);
        Task<IEnumerable<Invoice>> GetAll();
        Task Update(Guid invoiceId, InvoiceRequest request);
        Task<Guid> Create(InvoiceRequest request);
        Task<Money> GetTotalSum(Invoice invoice);
        Money CheckSum(Invoice invoice);

    }
}
