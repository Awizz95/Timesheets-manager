using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Data.Interfaces
{
    public interface IUserRepo
    {
        Task<User> GetByLoginAndPasswordHash(string login, byte[] passwordHash);
        Task CreateUser(User user);
        Task<string[]> GetUserRoleNamesAsync();
    }
}
