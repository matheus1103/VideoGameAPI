using VideoGameAPI.Entities;
using VideoGameAPI.Models;

namespace VideoGameAPI.Services
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(UserDto request);
        Task<TokenResponseDto?> LoginAsync(UserDto request);
        Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto refreshToken);
    }
}
