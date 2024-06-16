using System.Security.Claims;
using TimesheetsProj.Models.Dto.Responses;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Domain.Managers.Interfaces
{
    public interface ILoginManager
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
