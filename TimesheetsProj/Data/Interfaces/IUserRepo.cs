using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Data.Interfaces
{
    public interface IUserRepo
    {
        Task<User?> GetByEmailAndPasswordHash(string login, byte[] passwordHash);
        Task Create(User user);
        Task<string[]> GetUserRoleNamesAsync();
        Task<User?> GetByUserId(Guid userId);
        Task<IEnumerable<User>> GetAll();
        Task Update(User user);
    }
}
