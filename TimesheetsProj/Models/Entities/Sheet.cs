namespace TimesheetsProj.Models.Entities
{
    /// Информация о затраченном времени сотрудника
    public class Sheet : Entity
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid ContractId { get; set; }
        public Guid ServiceId { get; set; }
        public Guid? InvoiceId { get; set; }
        public int Amount { get; set; }
        public bool IsApproved { get; set; }
        public DateTime ApprovedDate { get; set; }

        public Employee Employee { get; set; } = null!;
        public Contract Contract { get; set; } = null!;
        public Service Service { get; set; } = null!;
        public Invoice? Invoice { get; set; }
    }
}
