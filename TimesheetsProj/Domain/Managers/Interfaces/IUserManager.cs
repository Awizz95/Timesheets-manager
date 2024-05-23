using TimesheetsProj.Models.Dto.Requests;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Domain.Managers.Interfaces
{
    public interface IUserManager
    {
        Task<User?> GetUserByRequest(LoginRequest request);
        Task<Guid> CreateUser(CreateUserRequest request);
        Task Update(Guid userId, UpdateUserRequest request);
        Task<User?> GetUserById(Guid userId);
    }
}
