using Application.Dtos.Auth;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using Infrastructure.Configurations;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services.Auth;

public partial class JwtAuthServiceAsync(
    IUsersRepositoryAsync usersRepositoryAsync,
    IWhitelistRepository whitelistRepository,
    IBlacklistRepository blacklistRepository,
    IHashService hashService,
    IOptions<JwtAccessTokenConfigurations> jwtAccessTokenConfigurations
    ) : IAuthServiceAsync
{
    private readonly JwtAccessTokenConfigurations _jwtAccessTokenConfigurations = jwtAccessTokenConfigurations.Value;
    public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
    {
        User? user = await usersRepositoryAsync.GetFirstOrDefaultAsync
        (
            x => x.Username == loginRequestDto.Username
        );

        if (user == null)
        {
            return null;
        }

        if (!hashService.Verify(loginRequestDto.Password, user.Password))
        {
            return null;
        }

        string token = GenerateToken(user);

        whitelistRepository.Add(token, true, TimeSpan.FromMinutes(_jwtAccessTokenConfigurations.LifetimeInMinutes));

        return new LoginResponseDto
        {
            Token = token,
        };
    }

    public async Task LogoutAsync(string token)
    {
        await Task.Run(() =>
        {
            whitelistRepository.Remove(token);
            blacklistRepository.Add(token, true, TimeSpan.FromMinutes(_jwtAccessTokenConfigurations.LifetimeInMinutes));
        });
    }
}