namespace TimesheetsProj.Models.Entities
{
    /// Информация о предоставляемой услуге в рамках контракта
    public class Service : Entity
    {
        public Service()
        {
            Sheets = [];
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Cost { get; set; }
        public ICollection<Sheet> Sheets { get; set; }
    }
}
