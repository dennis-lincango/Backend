using Application.Dtos.Auth;

namespace Application.Interfaces.Services;

public interface IAuthServiceAsync
{
    public Task<LoginResponseDto?> LoginAsync(LoginRequestDto loginRequestDto);
    public Task LogoutAsync(string token);
}
