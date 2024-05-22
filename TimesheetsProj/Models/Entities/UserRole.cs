namespace TimesheetsProj.Models.Entities
{
    public class UserRole : Entity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
