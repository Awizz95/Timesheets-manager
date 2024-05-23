using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;
using TimesheetsProj.Data.Ef;
using TimesheetsProj.Data.Interfaces;
using TimesheetsProj.Models.Dto;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Data.Implementation
{
    public class UserRepo : IUserRepo
    {
        private readonly TimesheetDbContext _dbContext;

        public UserRepo(TimesheetDbContext context)
        {
            _dbContext = context;
        }

        public async Task<User?> GetByLoginAndPasswordHash(string login, byte[] passwordHash)
        {
            return await _dbContext.Users
                    .Where(x => x.Username == login && x.PasswordHash == passwordHash)
                    .FirstOrDefaultAsync();
        }

        public async Task<User?> GetByUserId(Guid userId)
        {
            return await _dbContext.Users
                    .Where(x => x.Id == userId)
                    .FirstOrDefaultAsync();
        }

        public async Task CreateUser(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<string[]> GetUserRoleNamesAsync()
        {
            return await _dbContext.UserRoles.Select(x => x.Name).ToArrayAsync();
        }

        public async Task Update(User user)
        {
            await _dbContext.Users.Where(x => x.Id == user.Id).ExecuteUpdateAsync(x => x
                .SetProperty(x => x.Username, user.Username)
                .SetProperty(x => x.PasswordHash, user.PasswordHash)
                .SetProperty(x => x.Role, user.Role));

            await _dbContext.SaveChangesAsync();
        }
    }
}
