using TimesheetsProj.Data.Implementation;
using TimesheetsProj.Data.Interfaces;
using TimesheetsProj.Domain.Managers.Interfaces;
using TimesheetsProj.Domain.Mapper;
using TimesheetsProj.Models.Dto.Requests;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Domain.Managers.Implementation
{
    public class InvoiceManager : IInvoiceManager
    {
        private readonly IInvoiceRepo _invoiceRepo;

        public InvoiceManager(IInvoiceRepo invoiceRepo, ISheetRepo sheetRepo)
        {
            _invoiceRepo = invoiceRepo;
        }

        public async Task<Guid> Create(InvoiceRequest request)
        {
            Invoice invoice = InvoiceMapper.InvoiceRequestToInvoice(request);
            await _invoiceRepo.Create(invoice);

            return invoice.Id;
        }

        public async Task<Invoice> Get(Guid invoiceId)
        {
            Invoice? invoice = await _invoiceRepo.Get(invoiceId);

            if (invoice is not null) return invoice;

            throw new InvalidOperationException($"Счет с id: {invoiceId} не найден!");
        }

        public async Task<IEnumerable<Invoice>> GetAll()
        {
            IEnumerable<Invoice>? invoices = await _invoiceRepo.GetAll();

            if (!invoices.Any()) throw new InvalidOperationException("Список счетов пустой!");

            return invoices;
        }

        public async Task Update(Guid invoiceId, InvoiceRequest request)
        {
            Invoice invoice = InvoiceMapper.InvoiceRequestToUpdateInvoice(invoiceId, request);

            await _invoiceRepo.Update(invoice);
        }
    }
}
