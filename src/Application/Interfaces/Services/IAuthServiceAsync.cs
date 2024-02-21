using Application.Dtos.Auth;

namespace Application.Interfaces.Services
{
    /// <summary>
    /// Interface for asynchronous authentication service.
    /// </summary>
    public interface IAuthServiceAsync
    {
        /// <summary>
        /// Asynchronously performs login.
        /// </summary>
        /// <param name="loginRequestDto">The login request data transfer object.</param>
        /// <returns>The login response data transfer object, or null if login failed.</returns>
        public Task<LoginResponseDto?> LoginAsync(LoginRequestDto loginRequestDto);

        /// <summary>
        /// Asynchronously performs logout.
        /// </summary>
        /// <param name="token">The authentication token to be logged out.</param>
        public Task LogoutAsync(string token);
    }
}