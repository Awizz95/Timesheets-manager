using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;
using TimesheetsProj.Data.Ef;
using TimesheetsProj.Data.Interfaces;
using TimesheetsProj.Models;
using TimesheetsProj.Models.Dto;
using TimesheetsProj.Models.Entities;
using Contract = TimesheetsProj.Models.Entities.Contract;

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
                .SetProperty(x => x.Username, user.Username)
                .SetProperty(x => x.PasswordHash, user.PasswordHash)
                .SetProperty(x => x.Role, user.Role));

            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> CheckUserIsDeleted(Guid id)
        {
            User user = await _dbContext.Users.Where(x => x.Id == id).SingleAsync();
            bool status = user.IsDeleted;

            return status;
        }

        public async Task<IEnumerable<Contract>> GetAllContracts(Guid clientId)
        {
            User? user = await GetByUserId(clientId);

            if (user is null || user.Role != UserRoles.Client.ToString())
                throw new InvalidOperationException($"Пользователя с id: {clientId} не существует или он является клиентом");

            IEnumerable<Contract> contracts = await _contractRepo.GetAllByClient(clientId);

            return contracts;
        }

        public async Task<IEnumerable<Sheet>> GetAllSheets(Guid employeeId)
        {
            User? user = await GetByUserId(employeeId);

            if (user is null || user.Role != UserRoles.Employee.ToString())
                throw new InvalidOperationException($"Пользователя с id: {employeeId} не существует или он является работником");

            IEnumerable<Sheet> sheets = await _sheetRepo.GetAllByEmployee(employeeId);

            return sheets;
        }
    }
}
