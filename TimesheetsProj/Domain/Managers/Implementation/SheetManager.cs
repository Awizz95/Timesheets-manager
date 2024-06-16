using System.Diagnostics.Contracts;
using TimesheetsProj.Data.Implementation;
using TimesheetsProj.Data.Interfaces;
using TimesheetsProj.Domain.Managers.Interfaces;
using TimesheetsProj.Infrastructure.Mappers;
using TimesheetsProj.Models;
using TimesheetsProj.Models.Dto.Requests;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Domain.Managers.Implementation
{
    public class SheetManager : ISheetManager
    {
        private readonly ISheetRepo _sheetRepo;
        private readonly IServiceRepo _serviceRepo;
        private readonly IUserRepo _userRepo;

        public SheetManager(ISheetRepo sheetRepo, IServiceRepo serviceRepo, IUserRepo userRepo)
        {
            _sheetRepo = sheetRepo;
            _serviceRepo = serviceRepo;
            _userRepo = userRepo;
        }

        public async Task<Sheet> Get(Guid sheetId)
        {
            Sheet? sheet = await _sheetRepo.Get(sheetId);

            if (sheet is not null) return sheet;

            throw new InvalidOperationException($"Табель с id: {sheetId} не найден!");
        }

        public async Task<IEnumerable<Sheet>> GetAll()
        {
            IEnumerable<Sheet> sheets = await _sheetRepo.GetAll();

            if (!sheets.Any()) throw new InvalidOperationException("Список табелей пустой!");

            return sheets;
        }

        public async Task<Guid> Create(SheetRequest request)
        {
            Sheet sheet = SheetMapper.SheetRequestToSheet(request);
            await _sheetRepo.Create(sheet);

            return sheet.Id;
        }

        public async Task Update(Guid sheetId, SheetRequest request)
        {
            Sheet sheet = SheetMapper.SheetRequestToUpdateSheet(sheetId, request);
            await _sheetRepo.Update(sheet);
        }

        public async Task Approve(Sheet sheet, DateTime approvedDate)
        {
            sheet.IsApproved = true;
            sheet.ApprovedDate = approvedDate;
            await _sheetRepo.Update(sheet);
        }

        public async Task<decimal> CalculateSum(Sheet sheet)
        {
            Guid serviceId = sheet.ServiceId;
            Service service;

            try
            {
                service = await _serviceRepo.Get(serviceId);
            }
            catch
            {
                throw;
            }

            decimal cost = service.Cost;
            decimal sum = sheet.Amount * cost;

            return sum;
        }

        public async Task IncludeInvoice(Guid sheetId, Guid invoiceId)
        {
            await _sheetRepo.IncludeInvoice(sheetId, invoiceId);
        }

        public async Task<IEnumerable<Sheet>> GetSheetsForInvoice(Guid invoiceId)
        {
            IEnumerable<Sheet> result = await _sheetRepo.GetSheetsForInvoice(invoiceId);

            return result;
        }

        public async Task<IEnumerable<Sheet>> GetAllByEmployee(Guid employeeId)
        {
            IEnumerable<Sheet> sheets;

            try
            {
                User? user = await _userRepo.GetByUserId(employeeId);

                if (user is null || user.Role != UserRoles.Employee.ToString())
                    throw new InvalidOperationException($"Пользователя с id: {employeeId} не существует или он является работником");

                sheets = await _sheetRepo.GetAllByEmployee(employeeId);
            }
            catch (InvalidOperationException)
            {
                throw;
            }

            return sheets;
        }
    }
}
