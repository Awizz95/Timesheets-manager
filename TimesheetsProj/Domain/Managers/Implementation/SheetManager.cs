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

        public SheetManager(ISheetRepo sheetRepo)
        {
            _sheetRepo = sheetRepo;
        }

        public async Task<Sheet> GetItem(Guid id)
        {
            return await _sheetRepo.GetItem(id);
        }

        public async Task<IEnumerable<Sheet>> GetItems()
        {
            return await _sheetRepo.GetItems();
        }

        public async Task<Guid> Create(SheetRequest request)
        {
            var sheet = SheetMapper.SheetRequestToSheet(request);

            await _sheetRepo.Add(sheet);
            return sheet.Id;
        }

        public async Task Approve(Guid sheetId)
        {
            var sheet = await _sheetRepo.GetItem(sheetId);
            //sheet.ApproveSheet();
            await _sheetRepo.Update(sheetId, sheet);
        }

        public async Task Update(Guid sheetId, SheetRequest request)
        {
            var sheet = SheetMapper.SheetRequestToSheet(request);
            await _sheetRepo.Update(sheetId, sheet);
        }
    }
}
