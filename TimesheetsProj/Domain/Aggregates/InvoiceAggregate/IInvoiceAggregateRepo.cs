namespace TimesheetsProj.Domain.Aggregates.InvoiceAggregate
{
    public interface IInvoiceAggregateRepo
    {
        Task<InvoiceAggregate> GetItem(Guid id);
        Task<IEnumerable<InvoiceAggregate>> GetItems();
        Task Add(InvoiceAggregate item);
        Task Update(InvoiceAggregate item);
        Task<IEnumerable<SheetAggregate.SheetAggregate>> GetSheets(Guid contractId, DateTime dateStart, DateTime dateEnd);
    }
}
