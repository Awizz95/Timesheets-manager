using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Infrastructure.Auth
{
    public class JwtProvider : IJwtProvider
    {
        private readonly IConfiguration _configuration;
        public JwtProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            TokenValidationParameters tokenValidationParameters = GetTokenValidationParameters(false);
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken? securityToken);
            JwtSecurityToken? jwtSecurityToken = securityToken as JwtSecurityToken;

            //дополнительная проверка заголовка и алгоритма шифрования
            if (jwtSecurityToken is null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Неверный токен!");

            return principal;
        }

        public string GenerateAccessToken(User user)
        {
            SigningCredentials signingCredentials = new SigningCredentials(
                               new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:JwtOptions:SigningKey"]!)),
                               SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            DateTime now = DateTime.UtcNow;

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["Authentication:JwtOptions:Issuer"],
                audience: _configuration["Authentication:JwtOptions:Audience"],
                notBefore: now,
                claims: claims,
                expires: now.Add(TimeSpan.FromMinutes(_configuration.GetSection("Authentication:JwtOptions:Lifetime").Get<int>())),
                signingCredentials: signingCredentials);

            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }

        public string GenerateRefreshToken()
        {
            byte[] arrayOfNumbers = new byte[64];
            using RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(arrayOfNumbers);

            return Convert.ToBase64String(arrayOfNumbers);
        }

        public TokenValidationParameters GetTokenValidationParameters(bool lifetimeCheck)
        {
            return new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = _configuration["Authentication:JwtOptions:Issuer"],
                ValidateAudience = true,
                ValidAudience = _configuration["Authentication:JwtOptions:Audience"],
                ValidateLifetime = lifetimeCheck,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:JwtOptions:SigningKey"]!)),
                ValidateIssuerSigningKey = true,
                RoleClaimType = ClaimsIdentity.DefaultRoleClaimType, //проверить
                ClockSkew = TimeSpan.Zero
            };
        }
    }
}
