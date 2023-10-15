using System;
namespace RockyConnectBackend
{
    public class Verification
    {
        public Verification()
        {
        }
    }
    public class EmailVerification
    {
        public required string Email { get; set; }
        public required string Code { get; set; }
    }
    public class PhoneVerification
    {
        public required string PhoneNumber { get; set; }
    }
    public class OTP
    {
        public int ID { get; set; }
        public  string? Email { get; set; }
        public  string? Code { get; set; }
        public DateTime DateCreated { get; set; }
        public  string? Status { get; set; }


    }

}