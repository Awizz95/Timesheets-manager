using System.Diagnostics.Contracts;
using TimesheetsProj.Data.Implementation;
using TimesheetsProj.Data.Interfaces;
using TimesheetsProj.Domain.Managers.Interfaces;
using TimesheetsProj.Domain.Mapper;
using TimesheetsProj.Models.Dto.Requests;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Domain.Managers.Implementation
{
    public class SheetManager : ISheetManager
    {
        private readonly ISheetRepo _sheetRepo;
        private readonly IServiceRepo _serviceRepo;

        public SheetManager(ISheetRepo sheetRepo, IServiceRepo serviceRepo)
        {
            _sheetRepo = sheetRepo;
            _serviceRepo = serviceRepo;
        }

        public async Task<Sheet> Get(Guid sheetId)
        {
            Sheet? sheet = await _sheetRepo.Get(sheetId);
            if (sheet is not null) return sheet;

            throw new InvalidOperationException($"Табель с id: {sheetId} не найден!");
        }

        public async Task<IEnumerable<Sheet>> GetAll()
        {
            IEnumerable<Sheet>? sheets = await _sheetRepo.GetAll();

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
            Service service = await _serviceRepo.Get(serviceId);
            decimal cost = service.Cost;
            decimal sum = sheet.Amount * cost;
            return sum;
        }
    }
}
