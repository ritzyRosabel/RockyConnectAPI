using System;
namespace RockyConnectBackend.Model
{
	public class User
	{
        public int UserID { get; set; }
        public  string? FirstName { get; set; }
        public  string? LastName { get; set; }
        public  string? PhoneNumber{ get; set; }
        public  string? Password { get; set; }
        public  string? Email { get; set; }
        public Role Role { get; set; }
        public int AccountVerified { get; set; }
        public bool IsAccountActive { get; set; }
        public DateTime Date_Created { get; set; }
        public DateTime Date_Updated { get; set; }
        public DateTime Date_Verified { get; set; }
        // public int AccountVerified { get; set; }
        // public DateTime Date_Created { get; set; }
        //public DateTime Date_Updated { get; set; }
    }
    public class UserRequest
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public Role Role { get; set; }

        // public int AccountVerified { get; set; }
        // public DateTime Date_Created { get; set; }
        //public DateTime Date_Updated { get; set; }
    }
    public class UserUpdateRequest
    {
        public  string? FirstName { get; set; }
        public  string? LastName { get; set; }
        public  string ?PhoneNumber { get; set; }
        public  string? Password { get; set; }
        public required string Email { get; set; }
    }
        public class LoginUser
    {
        public  string? Email { get; set; }
        public  string? FirstName { get; set; }
        public  string? Password { get; set; }
        public Role Role { get; set; }
       
    }
    public class GetUserRequest
    {
        public string Email { get; set; }

    }
    public class Driver
    {
        public string Email { get; set; }
        public int Rating { get; set; }

    }
    public class LoginUserRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
    public class CreateUserResponse
    {
        public required string Status { get; set; }
        public int StatusCode { get; set; }
        public int CustomerID { get; set; }
    }
    public class Email
    {
        public required string UserEmail { get; set; }
        public int? Otptype  { get; set; }
    }
    public class PasswordResetRequest
    {
        public required string Email { get; set; }
        public required string OldPassword { get; set; }
        public required string NewPassword { get; set; }

    }
    public class PasswordForgotRequest
    {
        public required string Password { get; set; }
        public required string Email { get; set; }
    }
    public enum Role
    {
        rider = 1,
        driver = 2,
        both = 3
    }

}

