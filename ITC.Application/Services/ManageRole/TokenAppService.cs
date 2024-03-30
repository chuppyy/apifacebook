#region

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using ITC.Application.Interfaces.ManageRole;
using Microsoft.IdentityModel.Tokens;

#endregion

namespace ITC.Application.Services.ManageRole;

public class TokenAppService : ITokenAppService
{
#region ITokenAppService Members

    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        throw new NotImplementedException();
    }


    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token, byte[] key)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience =
                false, //you might want to validate the audience and issuer depending on your use case
            ValidateIssuer           = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey         = new SymmetricSecurityKey(key),
            ValidateLifetime         = false //here we are saying that we don't care about the token's expiration date
        };
        var           tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;
        var           principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
        var           jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                                                StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");
        return principal;
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

#endregion
}