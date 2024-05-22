using TimesheetsProj.Models.Dto.Requests;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Domain.Managers.Interfaces
{
    public interface IUserManager
    {
        /// <summary> Возвращает пользователя по логину и паролю </summary>
        Task<User> GetUser(LoginRequest request);

        /// <summary> Создает нового пользователя </summary>
        Task<Guid> CreateUser(CreateUserRequest request);
    }
}
