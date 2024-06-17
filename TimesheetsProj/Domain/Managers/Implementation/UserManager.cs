using TimesheetsProj.Data.Interfaces;
using TimesheetsProj.Domain.Managers.Interfaces;
using TimesheetsProj.Infrastructure;
using TimesheetsProj.Infrastructure.Mappers;
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

        public async Task<User?> GetUserByLoginRequest(LoginRequest request)
        {
            byte[] passwordHash = PasswordHasher.GetPasswordHash(request.Password);
            User? user = await _userRepo.GetByEmailAndPasswordHash(request.Email, passwordHash);

            return user;
        }

        public async Task<User?> GetUserById(Guid userId)
        {
            User? user = await _userRepo.GetByUserId(userId);

            if (user is not null) return user;

            throw new InvalidOperationException($"Пользователь с id {userId} не найден!");
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            User? user = await _userRepo.GetUserByEmail(email);

            return user;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            IEnumerable<User> users = await _userRepo.GetAll();

            if (!users.Any()) throw new InvalidOperationException("Список пользователей пустой!");

            return users;
        }

        public async Task<Guid> CreateUser(CreateUserRequest request)
        {
            string[] userRoleNames = await _userRepo.GetUserRoleNamesAsync();

            if (!userRoleNames.Contains(request.Role)) throw new InvalidOperationException("Введеная роль не существует!");

            User user = UserMapper.CreateUserRequestToUser(request);

            await _userRepo.Create(user);

            return user.Id;
        }

        public async Task Update(Guid userId, UpdateUserRequest request)
        {
            User? user = await GetUserById(userId) ?? throw new InvalidOperationException($"Пользователь с id: {userId} не найден!");

            bool isOldPasswordCorrect = PasswordHasher.IsPasswordCorrect(request.OldPassword, user.PasswordHash);

            if (!isOldPasswordCorrect) throw new InvalidOperationException("Действующий пароль не совпадает!");

            string[] userRoleNames = await _userRepo.GetUserRoleNamesAsync();

            if (!userRoleNames.Contains(request.Role)) throw new InvalidOperationException("Введеная роль не существует!");

            user = UserMapper.UpdateUserRequestToUser(userId,request);

            await _userRepo.Update(user);
        }

        public async Task Update(User user)
        {
            await _userRepo.Update(user);
        }
    }
}
