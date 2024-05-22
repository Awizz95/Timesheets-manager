using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Data.Interfaces
{
    public interface IContractRepo : IRepoBase<Contract>
    {
        Task<bool?> CheckContractIsActive(Guid id);
    }
}
