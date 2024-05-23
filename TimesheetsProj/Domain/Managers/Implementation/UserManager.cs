using System.Security.Cryptography;
using System.Text;
using TimesheetsProj.Data.Interfaces;
using TimesheetsProj.Domain.Managers.Interfaces;
using TimesheetsProj.Domain.Mappers;
using TimesheetsProj.Infrastructure.Extensions;
using TimesheetsProj.Models.Dto.Requests;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Domain.Managers.Implementation
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepo _userRepo;

        public UserManager(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<User?> GetUserByRequest(LoginRequest request)
        {
            byte[] passwordHash = GetPasswordHash(request.Password);
            User? user = await _userRepo.GetByLoginAndPasswordHash(request.Login, passwordHash);

            if (user is not null) return user;

            throw new InvalidOperationException("Ошибка при получении пользователя!");
        }

        public async Task<User?> GetUserById(Guid userId)
        {
            User? user = await _userRepo.GetByUserId(userId);

            if (user is not null) return user;

            throw new InvalidOperationException("Ошибка при получении пользователя!");
        }

        public async Task<Guid> CreateUser(CreateUserRequest request)
        {
            string[] userRoleNames = await _userRepo.GetUserRoleNamesAsync();

            if (!userRoleNames.Contains(request.Role)) throw new ApplicationException("Введеная роль не существует!");

            User user = UserMapper.CreateUserRequestToUser(request);
            await _userRepo.CreateUser(user);

            return user.Id;
        }

        public static byte[] GetPasswordHash(string password)
        {
            using (var sha1 = new SHA1CryptoServiceProvider())   // переделать
            {
                return sha1.ComputeHash(Encoding.Unicode.GetBytes(password));
            }
        }

        private async Task<bool> IsPasswordCorrect(string password, Guid userId)
        {

            byte[] passwordHash = GetPasswordHash(password);
            User? user = await _userRepo.GetByUserId(userId);

            if (user is null) throw new InvalidOperationException($"Пользователь с id: {userId} не найден!");

            bool result = user.PasswordHash == passwordHash;

            return result;

        }

        public async Task Update(Guid userId, UpdateUserRequest request)
        {
            bool isOldPasswordCorrect = await IsPasswordCorrect(request.OldPassword, userId);

            if (!isOldPasswordCorrect) throw new InvalidOperationException("Действующий пароль не совпадает!");

            string[] userRoleNames = await _userRepo.GetUserRoleNamesAsync();

            if (!userRoleNames.Contains(request.Role)) throw new ApplicationException("Введеная роль не существует!");

            User user = UserMapper.UpdateUserRequestToUser(userId,request);

            await _userRepo.Update(user);
        }
    }
}
