using TimesheetsProj.Data.Interfaces;
using TimesheetsProj.Domain.Aggregates.InvoiceAggregate;
using TimesheetsProj.Domain.Managers.Interfaces;
using TimesheetsProj.Models.Dto.Requests;

namespace TimesheetsProj.Domain.Managers.Implementation
{
    public class InvoiceManager : IInvoiceManager
    {
        private readonly IInvoiceRepo _invoiceRepo;
        private readonly IInvoiceAggregateRepo _invoiceAggregateRepo;

        public InvoiceManager(IInvoiceRepo invoiceRepo, ISheetRepo sheetRepo)
        {
            _invoiceRepo = invoiceRepo;
        }

        public async Task<Guid> Create(InvoiceRequest request)
        {
            var invoice = InvoiceAggregate.Create(request.ContractId, request.DateEnd, request.DateStart);

            var sheetsToInclude = await _invoiceAggregateRepo
                .GetSheets(request.ContractId, request.DateStart, request.DateEnd);

            invoice.IncludeSheets(sheetsToInclude);
            await _invoiceRepo.Add(invoice);

            return invoice.Id;
        }
    }
}
