using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;
using TimesheetsProj.Data.Ef;
using TimesheetsProj.Data.Interfaces;
using TimesheetsProj.Models;
using TimesheetsProj.Models.Dto;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Data.Implementation
{
    public class UserRepo : IUserRepo
    {
        private readonly TimesheetDbContext _dbContext;
        private readonly IContractRepo _contractRepo;
        private readonly ISheetRepo _sheetRepo;

        public UserRepo(TimesheetDbContext dbContext, IContractRepo contractRepo, ISheetRepo sheetRepo)
        {
            _dbContext = dbContext;
            _contractRepo = contractRepo;
            _sheetRepo = sheetRepo;
        }

        public async Task<User?> GetByEmailAndPasswordHash(string email, byte[] passwordHash)
        {
            return await _dbContext.Users
                    .Where(x => x.Email == email && x.PasswordHash == passwordHash)
                    .FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _dbContext.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
        }

        public async Task<User?> GetByUserId(Guid userId)
        {
            return await _dbContext.Users
                    .Where(x => x.Id == userId)
                    .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetAll() 
        {
            List<User> result = await _dbContext.Users.ToListAsync();

            return result;
        }

        public async Task Create(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<string[]> GetUserRoleNamesAsync()
        {
            string[] result = await _dbContext.UserRoles.Select(x => x.Name).ToArrayAsync();

            return result;
        }

        public async Task Update(User user)
        {
            await _dbContext.Users.Where(x => x.Id == user.Id).ExecuteUpdateAsync(x => x
                .SetProperty(x => x.Email, user.Email)
                .SetProperty(x => x.PasswordHash, user.PasswordHash)
                .SetProperty(x => x.Role, user.Role)
                .SetProperty(x => x.RefreshToken, user.RefreshToken)
                .SetProperty(x => x.RefreshTokenExpireTime, user.RefreshTokenExpireTime));

            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> CheckUserIsDeleted(Guid id)
        {
            User user = await _dbContext.Users.Where(x => x.Id == id).SingleAsync();
            bool status = user.IsDeleted;

            return status;
        }
    }
}
