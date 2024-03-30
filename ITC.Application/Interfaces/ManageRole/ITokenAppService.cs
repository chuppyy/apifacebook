#region

using System.Collections.Generic;
using System.Security.Claims;

#endregion

namespace ITC.Application.Interfaces.ManageRole;

public interface ITokenAppService
{
#region Methods

    string          GenerateAccessToken(IEnumerable<Claim> claims);
    string          GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token, byte[] key);

#endregion
}