namespace TimesheetsProj.Models.Entities
{
    /// <summary> Информация о сотруднике </summary>
    public class Employee : Entity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Sheet>? Sheets { get; set; }
    }
}
