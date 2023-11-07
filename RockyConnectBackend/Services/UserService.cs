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
                User result = UserData.GetUserUsingEmail(cred.Email);

                if (result.Email == null)
                {
                    status.statusCode = "01";
                    status.status = "Invalid Username and Password Match";
                    status.data = null;

                }
                else
                {

                    // Regex.Replace(result.UserName, @"\s+", "");
                    if (result.Password is not null)
                        if (UtilityService.CompareHashKeys(cred.Password.ToLower(),result.Password))
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
                            status.data = null;

                        }

                }
            }
            return status;
        }

        public static Response Create(UserRequest customer)
        {
            var response = new Response();
            string result; 
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
            
            string pass = UtilityService.HashKeys(user.Password.ToLower());
            user.Password = pass;
            result = UserData.CreateCustomerData(user);
            
            if (result == "00")
            {
                if (user.Role == Role.driver)
                {
                    Driver driver = new Driver() { Email=user.Email,Rating=5,NoOfRides=0} ;
                    UserData.CreateDriver(driver);

                }
                else
                {
                    UserData.SaveRider(user.Email);


                }
                SendOTP(user, "Email Verification");

                    response.statusCode = "00";
                    response.status = "OTP sent to email";

              

            }else if(result == "-2146232060")
            {
                response.statusCode = "01";
                response.status = "Account with email already exist";
            }
            else
            {
                response.statusCode = "01";
                response.status = "request was  unsuccessful";
            }
            return response;
        }

        private static string SendOTP(User user, string subject)
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
                send = UtilityService.SendEmail(messageBody, user.Email, subject);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return send;
        }

        internal static Response ValidateAccount(string code, string email)
        {
            var response = new Response();
            string result = string.Empty;
            User user = UserData.GetUserUsingEmail(email);

            if (user.Email == string.Empty)
            {
                response.statusCode = "01";
                response.status = "request was unsuccessful. User not found";
                return response;
            }
            Response res = ValidateOTP(code, email);
            if (res.statusCode == "00")
            {
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
                        response.status = "Account verification was successful, but email was unsuccessfully sent.";
                        response.statusCode = "01";


                    }

                }
                else
                {
                    response.statusCode = "01";
                    response.status = "request was unsuccessful";
                }
            }
            else
            {
                return res;
            }
            return response;
        }

        //internal static Response VerifyPhone(PhoneVerification number)
        //{
        //    Response response = new Response();
        //    string MessageBody = "<p> Hi, </p> <p> Your account has been successfully verified. No further action is neededfrome your end.</p>";
        //    string result = UtilityService.SendPhone(MessageBody, number.PhoneNumber);
        //    if (result == "00")
        //    {
        //        response.status = "Account Successfully Verified";
        //        response.statusCode = "00";


        //    }
        //    else
        //    {
        //        response.status = "Account verification was unsuccessfull, try again later. ";
        //        response.statusCode = "01";


        //    }
        //    return response;
        //}

        internal static Response ValidateOTP(string code, string email)
        {
            Response response = new Response();
            OTP result = UserData.GetUserOtp(code, email);
            
                if (result.ID != 0)
                {
                    TimeSpan time = DateTime.Now - result.DateCreated;
                    if (time.TotalMinutes >= 15)
                    {
                        result.Status = "Expired";

                        string result2 = UserData.UpdateOTP(result.Email, result.Code, result.Status);
                        response.statusCode = "01";
                        response.status = "Expired otp";
                        response.data = null;

                    }
                    else
                    {
                        result.Status = "Verified";
                        string result2 = UserData.UpdateOTP(result.Email, result.Code, result.Status);
                        if (result2 == "00")
                        {
                            response.statusCode = "00";
                            response.status = "Verified otp";
                            response.data = null;
                        }
                        else
                        {
                            response.statusCode = "01";
                            response.status = "unable to verify otp try again";
                            response.data = null;
                        }
                    }

                }
                else
                {
                    response.statusCode = "01";
                    response.status = "request was unsuccessful. User not found";
                    response.data = email;
                }


            
            return response;
        }

        internal static Response ResendOTP(Email email)
        {
            Response response = new Response();
            User user = new User();
            user.Email = email.UserEmail;
            if (email.Otptype == 1)
            {
                string result = SendOTP(user, "Password Reset");
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
            }
            else {
                string result = SendOTP(user, "Email Verification");
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
                response.status = "request was unsuccessful. User not found";
                return response;
            }


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
                    response.status = "Account Deletion was successful, but email was unsuccessfully sent.";
                    response.statusCode = "01";


                }

            }
            else
            {
                response.statusCode = "01";
                response.status = "request was unsuccessful.";
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

                result = UserData.UpdateData(user);
                if (result == "00")
                {
                    response.statusCode = "00";
                    response.status = "Profile was Successfully Updated";

                }
                else
                {
                    response.statusCode = "01";
                    response.status = "Profile update was unsuccessful";
                }
            }
            else {
                response.statusCode = "01";
                response.status = "User account not found";
            }
            return response;

        }

        internal static Response ForgotPassword(PasswordForgotRequest request)
        {
            Response response = new Response();
            // User user = new User();
            string result;

            User user = UserData.GetUserUsingEmail(request.Email);
            if (user.Email is not null)
            {
                string pass = UtilityService.HashKeys(request.Password.ToLower());
                user.Password = pass;


                result = UserData.UpdateData(user);
                if (result == "00")
                {
                    response.statusCode = "00";
                    response.status = "Password Reset Successful";

                }
                else
                {
                    response.statusCode = "01";
                    response.status = "Password Reset Failed";
                }
            }
            else
            {
                response.statusCode = "01";
                response.status = "request was unsuccessful. User not found";
            }
            return response;

        }

        internal static Response GetUserAccount(string email)
        {
            var status = new Response();

            User result = UserData.GetUserUsingEmail(email);
            if (result.Email is not null) {
                status.statusCode = "00";
                status.status = "Successfull";
                result.Password = "";
                status.data = result;
            }
            else
            {

                status.statusCode = "01";
                status.status = "No User Account with that email exist";
                status.data = null;
            }
            return status;
        }
        internal static Response GetDriver(string email)
        {
            var status = new Response();

            Driver result = UserData.GetDriver(email);
            if (result.Email is not null)
            {
                status.statusCode = "00";
                status.status = "Successfull";
                status.data = result;
            }
            else
            {

                status.statusCode = "00";
                status.status = "Successfull";
                status.data = null;
            }
            return status;
        }

        internal static Response VerifyOtp(VerifyOTP request)
        {
            throw new NotImplementedException();
        }
    }
    }