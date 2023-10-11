using System;
namespace RockyConnectBackend.Model
{
	public class User
	{
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber{ get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public int AccountVerified { get; set; }
        public DateTime Date_Created { get; set; }
        public DateTime Date_Updated { get; set; }
        public DateTime Date_Verified { get; set; }
        // public int AccountVerified { get; set; }
        // public DateTime Date_Created { get; set; }
        //public DateTime Date_Updated { get; set; }
    }
    public class UserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }

        // public int AccountVerified { get; set; }
        // public DateTime Date_Created { get; set; }
        //public DateTime Date_Updated { get; set; }
    }
    public class LoginUser
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
       
    }
    public class LoginUserResponse
    {
        public int UserID { get; set; }

    }
    public class LoginUserRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class CreateUserResponse
    {
        public string Status { get; set; }
        public int StatusCode { get; set; }
        public int CustomerID { get; set; }
    }
  
    public enum Role
    {
        rider = 1,
        driver = 2,
        both = 3
    }

}

