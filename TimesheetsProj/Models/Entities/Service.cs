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
        public required string Name { get; set; }
        public required decimal Cost { get; set; }
        public ICollection<Sheet> Sheets { get; set; }
    }
}
