using Application.Dtos.Auth;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Configurations;
using WebApi.Extensions;

namespace WebApi.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(
    IHashService hashService,
    IUsersRepositoryAsync usersRepository,
    IAuthServiceAsync authService,
    IOptions<AuthCookiesConfigurations> authCookiesConfigurations
    ) : ControllerBase
{
    private readonly AuthCookiesConfigurations _authCookiesConfigurations = authCookiesConfigurations.Value;

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
    {
        string? token = this.GetAccessToken();
        if (token != null)
        {
            return Conflict("Already logged in");
        }

        LoginResponseDto? response = await authService.LoginAsync(loginRequestDto);

        if (response == null)
        {
            return Unauthorized();
        }

        Response.Cookies.Append(_authCookiesConfigurations.CookieName, response.Token, new CookieOptions()
        {
            SameSite = SameSiteMode.None,
            Secure = true,
            HttpOnly = true,
            Expires = DateTime.Now.AddMinutes(_authCookiesConfigurations.TimeoutInMinutes)
        });

        return Ok(response.Token);
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] LoginRequestDto loginRequestDto)
    {
        User user = new()
        {
            Username = loginRequestDto.Username,
            Password = hashService.Hash(loginRequestDto.Password)
        };
        await usersRepository.AddAsync(user);
        return Ok();
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        string? token = this.GetAccessToken();
        if (token == null)
        {
            return Unauthorized();
        }

        await authService.LogoutAsync(token);
        Response.Cookies.Append(_authCookiesConfigurations.CookieName, "", new CookieOptions()
        {
            SameSite = SameSiteMode.None,
            Secure = true,
            HttpOnly = true,
            Expires = DateTime.Now.AddDays(-1)
        });
        return NoContent();
    }
}
