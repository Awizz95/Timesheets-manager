﻿using TimesheetsProj.Models.Dto.Requests;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Domain.Managers.Interfaces
{
    public interface IUserManager
    {
        Task<User?> GetUserByLoginRequest(LoginRequest request);
        Task<User?> GetUserByEmail(string email);
        Task<IEnumerable<User>> GetAll();
        Task<Guid> CreateUser(CreateUserRequest request);
        Task Update(Guid userId, UpdateUserRequest request);
        Task Update(User user);
        Task<User?> GetUserById(Guid userId);
    }
}
