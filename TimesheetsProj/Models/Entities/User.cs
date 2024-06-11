namespace TimesheetsProj.Models.Entities
{
    /// Информация о пользователе системы
    public class User : Entity
    {
        public User()
        {
            Contracts = [];
            Sheets = [];
        }

        public Guid Id { get; set; }
        public required string Email { get; set; }
        public required byte[] PasswordHash { get; set; }
        public required string Role { get; set; }
        public bool IsDeleted { get; set; } = false;
        public ICollection<Contract> Contracts { get; set; }
        public ICollection<Sheet> Sheets { get; set; }
    }
}
