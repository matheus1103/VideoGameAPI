using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoGameAPI.Entities;
using VideoGameAPI.Models;
using VideoGameAPI.Services;

namespace VideoGameAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        public static User user = new();
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            var user = await authService.RegisterAsync(request);
            if (user is null)
            {
                return BadRequest("User already exists");
            }

            return Ok(user);
        }
        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDto>> Login(UserDto request)
        {
            var token = await authService.LoginAsync(request);
            if (token is null)
            {
                return BadRequest("Invalid username or password");
            }
            return Ok(token);
        }
        [HttpGet]
        [Authorize]
        public IActionResult AuthenticatedOnlyEndpoint()
        {
            var username = User.Identity?.Name;
            return Ok($"Hello, {username}. You have accessed an authenticated endpoint.");
        }
        [HttpGet("admin-only")]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminOnlyEndpoint()
        {
            var username = User.Identity?.Name;
            return Ok($"Hello, Admin. You have accessed an authenticated endpoint.");
        }
        [HttpGet("refresh-token")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequestDto request)
        {
            var result = await authService.RefreshTokensAsync(request);
            if (result is null || result.AccessToken is null || result.RefreshToken is null)
            {
                return Unauthorized("Invalid Refresh Token");
            }
            return Ok(result);
        }
    }
}