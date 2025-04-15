namespace Login.Dto
{
    public class OTPVerifyRequest
    {
            public required string Email { get; set; }
            public required string OtpCode { get; set; }

    }
}
