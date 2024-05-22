using TimesheetsProj.Models.Dto;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Domain.Managers.Interfaces
{
    public interface ILoginManager
    {
        Task<LoginResponse> Authenticate(User user);
    }
}
