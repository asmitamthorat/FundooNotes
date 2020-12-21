using FundooModelLayer;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace FundooServiceLayer.TokenAuthentification
{
    public interface ITokenManager
    {
        string GenerateToken(Account account);
        ClaimsPrincipal GetPrincipal(string token);
        int ValidateToken(string token);
    }
}
