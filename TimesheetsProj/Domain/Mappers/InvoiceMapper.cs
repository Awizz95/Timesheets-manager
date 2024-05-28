using TimesheetsProj.Models.Dto.Requests;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Domain.Mapper
{
    public static class InvoiceMapper
    {
        public static Invoice InvoiceRequestToInvoice(InvoiceRequest request)
        {
            return new Invoice
            {
                Id = Guid.NewGuid(),
                ContractId = request.ContractId,
                DateStart = request.DateStart,
                DateEnd = request.DateEnd
            };
        }

        public static Invoice InvoiceRequestToUpdateInvoice(Guid invoiceId, InvoiceRequest request)
        {
            return new Invoice
            {
                Id = invoiceId,
                ContractId = request.ContractId,
                DateStart = request.DateStart,
                DateEnd = request.DateEnd
            };
        }
    }
}
