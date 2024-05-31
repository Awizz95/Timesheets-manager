namespace TimesheetsProj.Models.Entities
{
    public class UserRole : Entity
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
    }
}
