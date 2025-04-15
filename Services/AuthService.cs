using Login.Dto;
using Login.Models;
using Login.Data;
using Microsoft.EntityFrameworkCore;

namespace Login.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;
        private readonly EmailService _emailService;
        private readonly SmsService _smsService;

        public UserService(ApplicationDbContext context, EmailService emailService, SmsService smsService)
        {
            _context = context;
            _emailService = emailService;
            _smsService = smsService;
        }

        //private readonly UserService _userService = userService;


        public async Task RegisterAsync(RegisterRequest request)
        {
            if (request.Password != request.ConfirmPassword)
                throw new Exception("Passwords do not match");

            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
                throw new Exception("Email already in use");

            var otp = new Random().Next(100000, 999999).ToString();

            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Country = request.Country,
                PasswordHash = request.Password,
                OtpCode = otp,
                OtpExpiration = DateTime.UtcNow.AddMinutes(5),
                IsVerified = false
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Deliver OTP based on preference
            if (request.OtpDeliveryMethod.Equals("sms", StringComparison.CurrentCultureIgnoreCase))
            {
                await _smsService.SendOtpAsync(user.PhoneNumber, otp);
            }
            else
            {
                await _emailService.SendOtpAsync(user.Email, otp);
            }
        }

        internal object Login(LoginRequest request)
        {
            throw new NotImplementedException();
        }

        internal void VerifyOtp(OTPVerifyRequest request)
        {
            throw new NotImplementedException();
        }

        // OTP verification and login stay the same as before
    }


}
