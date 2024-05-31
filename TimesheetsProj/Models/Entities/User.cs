namespace TimesheetsProj.Models.Entities
{
    /// Информация о пользователе системы
    public class User : Entity
    {
        public Guid Id { get; set; }
        public required string Username { get; set; }
        public required byte[] PasswordHash { get; set; }
        public required string Role { get; set; }
    }
}
