using TimesheetsProj.Models.Dto.Requests;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Infrastructure.Mappers
{
    public static class SheetMapper
    {
        public static Sheet SheetRequestToSheet(SheetRequest request)
        {
            return new Sheet
            {
                Id = Guid.NewGuid(),
                Date = request.Date,
                UserId = request.EmployeeId,
                ContractId = request.ContractId,
                ServiceId = request.ServiceId,
                IsApproved = false,
                Amount = request.Amount
            };
        }

        public static Sheet SheetRequestToUpdateSheet(Guid sheetId, SheetRequest request)
        {
            return new Sheet
            {
                Id = sheetId,
                Date = request.Date,
                UserId = request.EmployeeId,
                ContractId = request.ContractId,
                ServiceId = request.ServiceId,
                IsApproved = false,
                Amount = request.Amount
            };
        }
    }
}
