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
        private readonly ILogger _logger;

        public UserManager(IUserRepo userRepo, ILogger logger)
        {
            _userRepo = userRepo;
            _logger = logger;
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

            return user;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            User? user = await _userRepo.GetUserByEmail(email);

            return user;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            IEnumerable<User> users = await _userRepo.GetAll();

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

        public async Task Update(User user)
        {
            await _userRepo.Update(user);
        }

        public async Task<bool> CheckUserIsDeleted(string email)
        {
            User? user = await _userRepo.GetUserByEmail(email);

            if (user is null) throw new InvalidOperationException("Пользователь не найден");

            bool isDeleted = user.IsDeleted;

            return isDeleted;
        }

        public async Task<bool> CheckUpdateUserRequest(UpdateUserRequest request)
        {
            try
            {
                User? user = await GetUserByEmail(request.Email) ?? throw new InvalidOperationException($"Пользователь не найден!");

                bool isOldPasswordCorrect = PasswordHasher.IsPasswordCorrect(request.OldPassword, user.PasswordHash);

                if (!isOldPasswordCorrect) throw new InvalidOperationException("Действующий пароль не совпадает!");

                string[] userRoleNames = await _userRepo.GetUserRoleNamesAsync();

                if (!userRoleNames.Contains(request.Role)) throw new InvalidOperationException("Введеная роль не существует!");

                return true;
            }
            catch(InvalidOperationException e)
            {
                _logger.Log(LogLevel.Information, e.Message);

                return false;
            }
        }
    }
}
