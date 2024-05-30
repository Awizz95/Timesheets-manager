using TimesheetsProj.Data.Implementation;
using TimesheetsProj.Data.Interfaces;
using TimesheetsProj.Domain.Managers.Interfaces;
using TimesheetsProj.Domain.Mapper;
using TimesheetsProj.Domain.ValueObjects;
using TimesheetsProj.Models.Dto.Requests;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Domain.Managers.Implementation
{
    public class InvoiceManager : IInvoiceManager
    {
        private readonly IInvoiceRepo _invoiceRepo;
        private readonly ISheetManager _sheetManager;

        public InvoiceManager(IInvoiceRepo invoiceRepo, ISheetManager sheetManager)
        {
            _invoiceRepo = invoiceRepo;
            _sheetManager = sheetManager;
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
            IEnumerable<Invoice> invoices = await _invoiceRepo.GetAll();

            if (!invoices.Any()) throw new InvalidOperationException("Список счетов пустой!");

            return invoices;
        }

        public async Task Update(Guid invoiceId, InvoiceRequest request)
        {
            Invoice invoice = InvoiceMapper.InvoiceRequestToUpdateInvoice(invoiceId, request);

            await _invoiceRepo.Update(invoice);
        }

        public async Task<Money> GetTotalSum(Invoice invoice)
        {
            decimal amount = 0;

            List<Sheet> sheets = (List<Sheet>) await _sheetManager.GetSheetsForInvoice(invoice.Id);

            if (sheets.Count == 0) return Money.FromDecimal(0);

            try
            {
                foreach (Sheet sheet in sheets)
                {
                    amount += await _sheetManager.CalculateSum(sheet);
                }
            }
            catch
            {
                throw;
            }

            Money sum = Money.FromDecimal(amount);
            invoice.Sum = sum;

            return sum;
        }

        public Money CheckSum(Invoice invoice)
        {
            if (invoice.Sum is null) return Money.FromDecimal(0);

            return invoice.Sum;
        }


    }
}
