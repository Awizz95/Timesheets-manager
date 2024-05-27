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
        private readonly IClientRepo _clientRepo;
        private readonly IEmployeeRepo _employeeRepo;

        public UserManager(IUserRepo userRepo, IClientRepo clientRepo, IEmployeeRepo employeeRepo)
        {
            _userRepo = userRepo;
            _clientRepo = clientRepo;
            _employeeRepo = employeeRepo;
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

            switch (user.Role)
            {
                case "User": await _userRepo.Create(user);
                    break;
                case "Client":
                    await _userRepo.Create(user);
                    await _clientRepo.Create(user);
                    break;
                case "Employee":
                    await _userRepo.Create(user);
                    await _employeeRepo.Create(user);
                    break;
                default:
                    throw new InvalidOperationException("Выбранная роль недоступна либо не существует!");
            }; 
         
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

            bool result = user.PasswordHash.SequenceEqual(passwordHash);

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
