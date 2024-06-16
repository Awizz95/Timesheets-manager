﻿using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Infrastructure.Auth
{
    public interface IJwtProvider
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
        TokenValidationParameters GetTokenValidationParameters();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
