using TimesheetsProj.Models.Dto.Requests;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Domain.Mapper
{
    public static class SheetMapper
    {
        public static Sheet SheetRequestToSheet(SheetRequest request)
        {
            return new Sheet
            {
                Id = Guid.NewGuid(),
                Date = request.Date,
                EmployeeId = request.EmployeeId,
                ContractId = request.ContractId,
                ServiceId = request.ServiceId,
                IsApproved = false,
                Amount = request.Amount
            };
        }
    }
}
