using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TimesheetsProj.Domain.Managers.Interfaces;
using TimesheetsProj.Infrastructure.Auth;
using TimesheetsProj.Infrastructure.Extensions;
using TimesheetsProj.Models.Dto.Responses;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Domain.Managers.Implementation
{
    public class LoginManager : ILoginManager
    {
        private readonly IJwtProvider _jwtProvider;
        private readonly IConfiguration _configuration;

        public LoginManager(IJwtProvider jwtProvider, IConfiguration configuration)
        {
            _jwtProvider = jwtProvider;
            _configuration = configuration;
        }

        public string GenerateAccessToken(User user)
        {
            string aToken = _jwtProvider.GenerateAccessToken(user);

            return aToken;
        }

        public string GenerateRefreshToken()
        {
            string rToken = _jwtProvider.GenerateRefreshToken();

            return rToken;
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            TokenValidationParameters tokenValidationParameters = _jwtProvider.GetTokenValidationParameters(false);
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken? securityToken);
            JwtSecurityToken? jwtSecurityToken = securityToken as JwtSecurityToken;

            //дополнительная проверка заголовка и алгоритма шифрования
            if (jwtSecurityToken is null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Неверный токен!");

            return principal;
        }



    }
}
