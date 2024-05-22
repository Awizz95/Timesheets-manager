namespace TimesheetsProj.Models.Entities
{
    /// Информация о владельце контракта
    public class Client : Entity
    {
        public Guid Id { get; set; }
        public Guid User { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Contract>? Contract { get; set; }
    }
}
