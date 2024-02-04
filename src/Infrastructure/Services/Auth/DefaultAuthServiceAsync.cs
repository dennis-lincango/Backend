using Application.Dtos.Auth;
using Application.Interfaces.Services;

namespace Infrastructure.Services.Auth;

public class DefaultAuthServiceAsync : IAuthServiceAsync
{
    public Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
    {
        throw new NotImplementedException();
    }
}
