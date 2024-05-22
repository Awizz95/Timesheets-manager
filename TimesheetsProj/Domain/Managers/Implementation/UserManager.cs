using System.Security.Cryptography;
using System.Text;
using TimesheetsProj.Data.Interfaces;
using TimesheetsProj.Domain.Managers.Interfaces;
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

        public async Task<User> GetUser(LoginRequest request)
        {
            var passwordHash = GetPasswordHash(request.Password);
            var user = await _userRepo.GetByLoginAndPasswordHash(request.Login, passwordHash);

            return user;
        }

        public async Task<Guid> CreateUser(CreateUserRequest request)
        {
            request.EnsureNotNull(nameof(request));

            var userRoleNames = await _userRepo.GetUserRoleNamesAsync();

            if (!userRoleNames.Contains(request.Role)) throw new ApplicationException("Введеная роль не существует!");

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = request.Username,
                PasswordHash = GetPasswordHash(request.Password),
                Role = request.Role

            };

            await _userRepo.CreateUser(user);

            return user.Id;
        }

        private static byte[] GetPasswordHash(string password)
        {
            using (var sha1 = new SHA1CryptoServiceProvider())   // переделать
            {
                return sha1.ComputeHash(Encoding.Unicode.GetBytes(password));
            }
        }
    }
}
