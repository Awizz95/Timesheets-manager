using TimesheetsProj.Models.Dto.Requests;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Domain.Mapper
{
    public static class ContractMapper
    {
        public static Contract ContractRequestToContract(ContractRequest request)
        {
            return new Contract
            {
                Id = new Guid(),
                Title = request.Title,
                Description = request.Description,
                DateStart = request.DateStart,
                DateEnd = request.DateEnd,
                IsDeleted = false,
                Sheets = new List<Sheet>()
            };
        }
    }
}
