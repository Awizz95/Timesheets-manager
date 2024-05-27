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

            IEnumerable<Sheet> sheetsToInclude = await _invoiceRepo.GetSheets(invoice.Id);

            //_invoiceRepo.IncludeSheets(sheetsToInclude);
            await _invoiceRepo.Create(invoice);

            return invoice.Id;
        }
    }
}
