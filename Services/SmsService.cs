using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Login.Services
{
    public class SmsService
    {
        private readonly IConfiguration _config;

        public SmsService(IConfiguration config)
        {
            _config = config;
            TwilioClient.Init(_config["Twilio:AccountSid"], _config["Twilio:AuthToken"]);
        }

        public async Task SendOtpAsync(string toPhoneNumber, string otp)
        {
            await MessageResource.CreateAsync(
                body: $"Your OTP is: {otp}",
                from: new PhoneNumber(_config["Twilio:FromNumber"]),
                to: new PhoneNumber(toPhoneNumber)
            );
        }
    }

}
