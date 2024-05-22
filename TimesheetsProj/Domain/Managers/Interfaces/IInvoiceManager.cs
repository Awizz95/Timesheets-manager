using TimesheetsProj.Models.Dto.Requests;

namespace TimesheetsProj.Domain.Managers.Interfaces
{
    public interface IInvoiceManager
    {
        Task<Guid> Create(InvoiceRequest request);
    }
}
