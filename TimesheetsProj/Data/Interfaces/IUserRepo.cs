using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Data.Interfaces
{
    public interface IUserRepo
    {
        Task<User?> GetByLoginAndPasswordHash(string login, byte[] passwordHash);
        Task Create(User user);
        Task<string[]> GetUserRoleNamesAsync();
        Task<User?> GetByUserId(Guid userId);
        Task<IEnumerable<User>> GetAll();
        Task Update(User user);
        Task<IEnumerable<Contract>> GetAllContracts(Guid clientId);
        Task<IEnumerable<Sheet>> GetAllSheets(Guid employeeId);
    }
}
