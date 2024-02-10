using Microsoft.AspNetCore.Mvc;

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
        if(authorizationParts.Length != 2)
        {
            return null;
        }

        return authorizationParts[1];
    }
}
