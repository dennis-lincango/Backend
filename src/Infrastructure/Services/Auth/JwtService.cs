using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services.Auth;

public partial class JwtAuthServiceAsync
{
    private string GenerateToken(User user)
    {
        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_jwtAccessTokenConfigurations.Key));
        SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);
        JwtSecurityToken token = new(
            _jwtAccessTokenConfigurations.Issuer,
            _jwtAccessTokenConfigurations.Audience,
            [
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.UserType.ToString())
            ],
            expires: DateTime.Now.AddMinutes(_jwtAccessTokenConfigurations.LifetimeInMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private bool ValidateToken(string token)
    {
        JwtSecurityTokenHandler tokenHandler = new();
        TokenValidationParameters validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = _jwtAccessTokenConfigurations.Issuer,
            ValidateAudience = true,
            ValidAudience = _jwtAccessTokenConfigurations.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtAccessTokenConfigurations.Key)),
            ValidateLifetime = true
        };

        try
        {
            tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
            return true;
        }
        catch (SecurityTokenException)
        {
            return false;
        }
    }
}
