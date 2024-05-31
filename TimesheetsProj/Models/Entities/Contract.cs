using System.Text.Json.Serialization;

namespace TimesheetsProj.Models.Entities
{
    /// Информация о договоре с клиентом
    public class Contract : Entity
    {
        public Contract()
        {
            Sheets = [];
        }

        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required DateTime DateStart { get; set; }
        public required DateTime DateEnd { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; } = false;
        public ICollection<Sheet> Sheets { get; set; }
    }
}
