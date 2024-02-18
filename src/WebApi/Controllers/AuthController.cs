using Application.Dtos.Auth;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using Domain.Enums;
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
    IOptions<AuthCookiesConfigurations> authCookiesConfigurations,
    ILogger<AuthController> logger
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
            logger.LogWarning($"Failed login: A failed login attempt from {loginRequestDto.Username} was made.");
            return Unauthorized();
        }

        Response.Cookies.Append(_authCookiesConfigurations.CookieName, response.Token, new CookieOptions()
        {
            SameSite = SameSiteMode.None,
            Secure = true,
            HttpOnly = true,
            Expires = DateTime.Now.AddMinutes(_authCookiesConfigurations.TimeoutInMinutes)
        });

        logger.LogInformation($"Successful login: A successful login attempt from {loginRequestDto.Username} was made.");
        return Ok(response.Token);
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] LoginRequestDto loginRequestDto)
    {
        User user = new()
        {
            Username = loginRequestDto.Username,
            Password = hashService.Hash(loginRequestDto.Password),
            UserType = UserType.Administrative
        };
        await usersRepository.AddAsync(user);
        logger.LogInformation($"Successful registration: A successful registration attempt from {loginRequestDto.Username} was made.");
        return Ok();
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        string? token = this.GetAccessToken();
        string? username = this.GetAuthTokenUser();
        if (token == null)
        {
            logger.LogInformation($"Unauthorized logout: An unauthorized logout attempt from {username} was made.");
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
        logger.LogInformation($"Successful logout: A successful logout attempt from {username} was made.");
        return NoContent();
    }

    [HttpHead("is-logged-in")]
    [Authorize]
    public IActionResult IsLoggedIn()
    {
        return Ok();
    }

    [HttpHead("is-administrative")]
    [Authorize(Roles = "Administrative")]
    public IActionResult IsAdministrative()
    {
        return Ok();
    }

    [HttpHead("is-operational")]
    [Authorize(Roles = "Operational")]
    public IActionResult IsOperational()
    {
        return Ok();
    }
}
