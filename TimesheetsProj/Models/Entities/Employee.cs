namespace TimesheetsProj.Models.Entities
{
    /// <summary> Информация о сотруднике </summary>
    public class Employee : Entity
    {
        public Employee()
        {
            Sheets = [];
        }

        public Guid Id { get; set; }
        public required Guid UserId { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Sheet> Sheets { get; set; }
    }
}
