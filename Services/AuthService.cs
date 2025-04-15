using Login.Dto;
using Login.Models;

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
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                OtpCode = otp,
                OtpExpiration = DateTime.UtcNow.AddMinutes(5),
                IsVerified = false
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Deliver OTP based on preference
            if (request.OtpDeliveryMethod.ToLower() == "sms")
            {
                await _smsService.SendOtpAsync(user.PhoneNumber, otp);
            }
            else
            {
                await _emailService.SendOtpAsync(user.Email, otp);
            }
        }

        // OTP verification and login stay the same as before
    }


}
