using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WebApi.Extensions;

public static class AuthorizationExtensions
{
    public static string? GetAccessToken(this ControllerBase controllerBase)
    {
        string? authorizationHeader = controllerBase.Request.Headers.Authorization;

        if (authorizationHeader == null)
        {
            return null;
        }

        string[] authorizationParts = authorizationHeader.Split(" ");
        if (authorizationParts.Length != 2)
        {
            return null;
        }

        return authorizationParts[1];
    }

    public static string? GetAuthTokenUser(this ControllerBase controllerBase)
    {
        string? accessToken = controllerBase.GetAccessToken();
        if (accessToken == null)
        {
            return null;
        }
        
        JwtSecurityTokenHandler tokenHandler = new();
        JwtSecurityToken? jwtToken = tokenHandler.ReadToken(accessToken) as JwtSecurityToken;

        if (jwtToken == null)
        {
            return null;
        }

        string? nameClaim = jwtToken.Claims.First(c => c.Type == ClaimTypes.Name).Value;

        return nameClaim;
    }
}
