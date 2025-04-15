namespace Login.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string OtpCode { get; set; } = null!;
        public DateTime OtpExpiration { get; set; }
        public bool IsVerified { get; set; } = false;
    }

}
