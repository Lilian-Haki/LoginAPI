using Login.Dto;
using Login.Services;
using Microsoft.AspNetCore.Mvc;

namespace Login.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _userService = new();

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            await _userService.RegisterAsync(request);
            return Ok("OTP sent");
        }

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpRequest request)
        {
            await _userService.VerifyOtpAsync(request);
            return Ok("Account verified");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _userService.Login(request);
            return Ok($"Welcome {user.FirstName}");
        }
    }

}
