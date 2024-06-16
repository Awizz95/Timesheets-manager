using TimesheetsProj.Models.Dto.Requests;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Infrastructure.Mappers
{
    public static class ContractMapper
    {
        public static Contract ContractRequestToContract(ContractRequest request)
        {
            return new Contract
            {
                Id = new Guid(),
                ClientId = request.ClientId,
                Title = request.Title,
                Description = request.Description,
                DateStart = request.DateStart,
                DateEnd = request.DateEnd,
                IsDeleted = false,
                Sheets = new List<Sheet>()
            };
        }

        public static Contract ContractRequestToUpdateContract(Guid contractId, ContractRequest request)
        {
            return new Contract
            {
                Id = contractId,
                ClientId = request.ClientId,
                Title = request.Title,
                Description = request.Description,
                DateStart = request.DateStart,
                DateEnd = request.DateEnd,
                IsDeleted = false,
            };
        }
    }
}
