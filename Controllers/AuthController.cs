using Login.Dto;
using Login.Services;
using Microsoft.AspNetCore.Mvc;

namespace Login.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            await _userService.RegisterAsync(request);
            return Ok("OTP sent");
        }

        [HttpPost("verify-otp")]
        public IActionResult VerifyOtp([FromBody] OTPVerifyRequest request)
        {
            _userService.VerifyOtp(request);
            return Ok("Account verified");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _userService.Login(request);
            return Ok(value: $"Welcome");
        }
    }

}
