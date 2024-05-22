namespace TimesheetsProj.Models.Entities
{
    /// Информация о предоставляемой услуге в рамках контракта
    public class Service : Entity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<Sheet> Sheets { get; set; }
    }
}
