namespace TimesheetsProj.Models.Entities
{
    /// Информация о затраченном времени сотрудника
    public class Sheet : Entity
    {
        public Guid Id { get; set; }
        public required DateTime Date { get; set; }
        public required Guid EmployeeId { get; set; }
        public required Guid ContractId { get; set; }
        public required Guid ServiceId { get; set; }
        public Guid? InvoiceId { get; set; }
        public required int Amount { get; set; }
        public bool IsApproved { get; set; } = false;
        public DateTime ApprovedDate { get; set; }
        public Contract Contract { get; set; }
        public Service Service { get; set; }
        public Invoice? Invoice { get; set; }
    }
}
