using RockyConnectBackend.Data;
using RockyConnectBackend.Model;
using RockyConnectBackend.Services;

namespace RockyConnectBackend.Controllers
{
    internal class UserService
    {
        public static Response Login(LoginUserRequest cred)
        {
            var status = new Response();
            if (cred.Email != null)
            {
                LoginUser result = UserData.LoginData(cred.Email);
                if (result.Email == null)
                {
                    User result2 = UserData.GetUserUsingEmail(cred.Email);
                    if (result2.Email == null)
                    {
                        status.statusCode = "02";
                        status.status = "User Account Doesnt exist";
                    }
                    else
                    {
                        if (result2.Password.Trim().ToLower() == cred.Password.ToLower())
                        {
                            string result3 = UserData.CreateLoginData(result2);
                            result.Password = "";
                            status.statusCode = "00";
                            status.status = "Successfull";
                            status.data = result3;

                        }
                        else
                        {
                            status.statusCode = "01";
                            status.status = "Invalid Username and Password Match";
                            status.data = 0;

                        }
                    }

                }
                else
                {
                    // Regex.Replace(result.UserName, @"\s+", "");
                    if (result.Password.Trim().ToLower() == cred.Password.ToLower())
                    {
                        result.Password = "";
                        status.statusCode = "00";
                        status.status = "Successfull";
                        status.data = result;

                    }
                    else
                    {
                        status.statusCode = "01";
                        status.status = "Invalid Username and Password Match";
                        status.data = 0;

                    }

                }
            }
            return status;
        }

        public static Response Create(UserRequest customer)
        {
            var response = new Response();
            string result = string.Empty;
            User user = new User
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PhoneNumber = customer.PhoneNumber,
                Email = customer.Email,
                Password = customer.Password,
                Role = customer.Role,
                IsAccountActive = true
            };
            result = UserData.CreateCustomerData(user);
            if (result == "00")
            {

                string result2 = UserData.CreateLoginData(user);
                if (result2 == "00")
                {
                    SendEmailVerifyOTP(user);

                    response.statusCode = "00";
                    response.status = "OTP sent to email";

                }
                else
                {
                    SendEmailVerifyOTP(user);

                    response.statusCode = "02";
                    response.status = "Successfull created, OTP sent | but couldnt generate LoginID";
                }

            }
            else
            {
                response.statusCode = "01";
                response.status = "request was  unsuccessful";
            }
            return response;
        }

        private static string SendEmailVerifyOTP(User user)
        {
            string send = "01";
            string code = UtilityService.RandomOTPGenerator();
            string result = UserData.SaveOTP(user.Email, code);
            string messageBody;
            if (user.FirstName == string.Empty)
            {
                messageBody = $"<p> Hi, </p> <p> Your one-time RockyConnect OTP is {code}.<br/> This OTP is valid for the next 5 minutes.</p>";

            }
            else
            {

                messageBody = $"<p> Hi {user.FirstName}, </p> <p> Your one-time RockyConnect OTP is {code}.<br/> This OTP is valid for the next 5 minutes.</p>";

            }
            try
            {
                send = UtilityService.SendEmail(messageBody, user.Email, "OTP Verification");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return send;
        }

        internal static Response ValidateAccount(string email)
        {
            var response = new Response();
            string result = string.Empty;
            User user = UserData.GetUserUsingEmail(email);

            if (user.Email == string.Empty)
            {
                response.statusCode = "01";
                response.status = "request was  unsuccessful. User not found";
                return response;
            }

            user.Email = email;
            user.AccountVerified = 0;
            user.Date_Verified = DateTime.Now;

            result = UserData.VerifyAccount(user);
            if (result == "00")
            {
                string MessageBody = "<p> Hi, </p> <p> Your account has been successfully verified. No further action is needed from your end.</p>";
                string send = UtilityService.SendEmail(MessageBody, user.Email, "Account Verification");
                if (send == "00")
                {
                    response.status = "Account Successfully Verified";
                    response.statusCode = "00";


                }
                else
                {
                    response.status = "Account verification was successful, but email was unsuccessfully sent. ";
                    response.statusCode = "01";


                }

            }
            else
            {
                response.statusCode = "01";
                response.status = "request was  unsuccessful";
            }
            return response;
        }

        internal static Response VerifyPhone(PhoneVerification number)
        {
            Response response = new Response();
            string MessageBody = "<p> Hi, </p> <p> Your account has been successfully verified. No further action is neededfrome your end.</p>";
            string result = UtilityService.SendPhone(MessageBody, number.PhoneNumber);
            if (result == "00")
            {
                response.status = "Account Successfully Verified";
                response.statusCode = "00";


            }
            else
            {
                response.status = "Account verification was unsuccessfull, try again later. ";
                response.statusCode = "01";


            }
            return response;
        }

        internal static Response ValidateEmail(EmailVerification email)
        {
            Response response = new Response();
            OTP result = UserData.GetUserOtp(email);
            {
                if (result.ID != 0)
                {
                    TimeSpan time = DateTime.Now - result.DateCreated;
                    if (time.TotalMinutes >= 5)
                    {
                        result.Status = "Expired";
                        string result2 = UserData.UpdateOTP(result.Email, result.Code, result.Status);

                        response.statusCode = "01";
                        response.status = "Expired otp";
                        response.data = result;

                    }
                    else
                    {
                        result.Status = "Verified";
                        string result2 = UserData.UpdateOTP(result.Email, result.Code, result.Status);
                        if (result2 == "00")
                        {
                            response.statusCode = "00";
                            response.status = "Verified otp";
                            response.data = result;
                        }
                        else
                        {
                            response.statusCode = "02";
                            response.status = "unable to verify otp try again";
                            response.data = result;
                        }
                    }

                }
                else
                {
                    response.statusCode = "01";
                    response.status = "Invalid otp";
                    response.data = email;
                }


            }
            return response;
        }

        internal static Response ResendOTP(string email)
        {
            Response response = new Response();
            User user = new User();
            user.Email = email;
            string result = SendEmailVerifyOTP(user);
            if (result == "00")
            {
                response.statusCode = "00";
                response.status = "OTP sent to email";
                response.data = email;
            }
            else
            {
                response.statusCode = "01";
                response.status = "OTP failed to send";
                response.data = email;
            }
            return response;
        }

        internal static Response DeleteAccount(string email)
        {
            var response = new Response();
            string result = string.Empty;
            User user = UserData.GetUserUsingEmail(email);

            if (user.Email == string.Empty)
            {
                response.statusCode = "01";
                response.status = "request was  unsuccessful. User not found";
                return response;
            }

            user.Email = email;
            user.IsAccountActive = false;
            user.Date_Updated = DateTime.Now;

            result = UserData.DeleteAccount(user);
            if (result == "00")
            {
                string MessageBody = $"<p> Hi {user.FirstName}, </p> <p> We are sad to see you go, Your account has been successfully Deleted. <br/> No further action is needed from your end.</p>";
                string send = UtilityService.SendEmail(MessageBody, user.Email, "Account Verification");
                if (send == "00")
                {
                    response.status = "Account Successfully Deleted";
                    response.statusCode = "00";


                }
                else
                {
                    response.status = "Account Deletion was successful, but email was unsuccessfully sent. ";
                    response.statusCode = "01";


                }

            }
            else
            {
                response.statusCode = "01";
                response.status = "request was  unsuccessful";
            }
            return response;
        }

        internal static Response UpdateAccount(UserUpdateRequest customer)
        {
            var response = new Response();
            string result;

            User user = UserData.GetUserUsingEmail(customer.Email);
            if (user.Email is not null)
            {

                user.FirstName = customer.FirstName;
                user.LastName = customer.LastName;
                user.PhoneNumber = customer.PhoneNumber;
                user.Password = customer.Password;
            
            result = UserData.UpdateData(user);
            if (result == "00")
            {
                response.statusCode = "00";
                response.status = "Profile was Successfully Updated";

            }
            else
            {
                response.statusCode = "01";
                response.status = "Profile update was  unsuccessful";
            }
            }
            else{
                response.statusCode = "01";
                response.status = "User account not found";
            }
            return response;
        
            }
        
    }
}