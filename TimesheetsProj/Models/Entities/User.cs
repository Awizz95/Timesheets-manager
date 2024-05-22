namespace TimesheetsProj.Models.Entities
{
    /// Информация о пользователе системы
    public class User : Entity
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = null!;
        public byte[] PasswordHash { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}
