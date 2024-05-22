namespace TimesheetsProj.Models.Dto.Requests
{
    public class InvoiceRequest
    {
        public Guid ContractId { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
    }
}
