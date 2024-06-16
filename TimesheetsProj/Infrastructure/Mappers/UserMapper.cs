
using System.Data;
using TimesheetsProj.Domain.Managers.Implementation;
using TimesheetsProj.Models.Dto.Requests;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Infrastructure.Mappers
{
    public static class UserMapper
    {
        public static User CreateUserRequestToUser(CreateUserRequest request)
        {
            return new User
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                PasswordHash = PasswordHasher.GetPasswordHash(request.Password),
                Role = request.Role
            };
        }

        public static User UpdateUserRequestToUser(Guid id, UpdateUserRequest request)
        {
            return new User
            {
                Id = id,
                Email = request.Email,
                PasswordHash = PasswordHasher.GetPasswordHash(request.NewPassword),
                Role = request.Role
            };
        }
    }
}
