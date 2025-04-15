using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;

namespace Login.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendOtpAsync(string toEmail, string otp)
        {
            var client = new SendGridClient(_config["SendGrid:ApiKey"]);
            var from = new EmailAddress("no-reply@yourdomain.com", "Your App");
            var to = new EmailAddress(toEmail);
            var subject = "Your OTP Code";
            var content = new Content("text/plain", $"Your OTP is: {otp}");
            var msg = new SendGridMessage
            {
                From = from,
                Subject = subject,
                PlainTextContent = content.Value
            };
            msg.AddTo(to);
            var response = await client.SendEmailAsync(msg);
        }
    }

}
