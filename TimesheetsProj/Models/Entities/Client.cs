namespace TimesheetsProj.Models.Entities
{
    /// Информация о владельце контракта
    public class Client : Entity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Contract>? Contracts { get; set; }
    }
}
