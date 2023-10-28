//using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using RockyConnectBackend.Model;
using RockyConnectBackend.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RockyConnectBackend.Controllers
{
    [Route("/")]
    public class UserAccountController : Controller
    {
      /*  // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
      */
        // POST api/values
        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] UserRequest customer)
        {
           

            if (!UtilityService.IsPhoneNbr(customer.PhoneNumber) && !UtilityService.IsValidEmail(customer.Email))
            {
                return BadRequest("phone number or email invalid");
            }
            if (customer.FirstName==string.Empty || customer.LastName == string.Empty || customer.Password == string.Empty )
            {
                return BadRequest("Some required field empty.");

            }
            try
            {
            Response response = UserService.Create(customer);
                if (response.statusCode == "00")
                {
                    return Ok(response);
                }
                else
                {
                    return StatusCode(500, response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [HttpPost]
        [Route("Login")]

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Login([FromBody] LoginUserRequest cred)
        {
            if (  !UtilityService.IsValidEmail(cred.Email))
            {
                return BadRequest("phone number or email invalid");
            }
            try
            {
                Response response = UserService.Login(cred);
                if (response.statusCode == "00")
                {
                    return Ok(response);
                }
                else
                {
                    return StatusCode(500, response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }


        }
        [HttpPost]
        [Route("ResendOTP")]

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ResendOTP([FromBody] Email email )
        {
            try
            {
                Response response = UserService.ResendOTP(email);
                if (response.statusCode == "00")
                {
                    return Ok(response);
                }
                else
                {
                    return StatusCode(500, response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [HttpPost]
        [Route("VerifyEmail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult VerifyEmail([FromBody] EmailVerification cred)
        {

            if (!UtilityService.IsValidEmail(cred.Email))
            {
                return BadRequest("phone number or email invalid");
            }
            if (cred.Code.Length > 6 || cred.Code.Length < 6)
            {
                return BadRequest("Verification code is a six digit number");

            }
            try
            {
                Response response = UserService.ValidateAccount(cred.Code, cred.Email);
                if (response.statusCode == "00")
                {
                    return Ok(response);
                }
                else
                {
                    return StatusCode(500, response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet]
        [Route("GetUserAccount")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetUserAccount( string email)
        {

            if (!UtilityService.IsValidEmail(email))
            {
                return BadRequest("phone number or email invalid");
            }
            try
            {
                Response response = UserService.GetUserAccount(email);
                if (response.statusCode == "00")
                {
                    return Ok(response);
                }
                else
                {
                    return StatusCode(500, response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [HttpPut]
        [Route("UpdateAccount")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateAccount([FromBody] UserUpdateRequest request)
        {

            if (!UtilityService.IsValidEmail(request.Email))
            {
                return BadRequest("phone number or email invalid");
            }

            try
            {
                Response response = UserService.UpdateAccount(request);
                if (response.statusCode == "00")
                {
                    return Ok(response);
                }
                else
                {
                    return StatusCode(500, response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }


        }
        [HttpDelete]
        [Route("DeleteAccount")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteAccount(string email)
        {

            if (!UtilityService.IsValidEmail(email))
            {
                return BadRequest("phone number or email invalid");
            }

            try
            {
                Response response = UserService.DeleteAccount(email);
                if (response.statusCode == "00")
                {
                    return Ok(response);
                }
                else
                {
                    return StatusCode(500, response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [HttpPost]
        [Route("ForgotPassword")]

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ForgotPassword([FromBody] PasswordForgotRequest request)
        {
            try
            {
                
                Response response = UserService.ForgotPassword(request);
                if (response.statusCode == "00")
                {
                    return Ok(response);
                }
                else
                {
                    return StatusCode(500, response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [HttpPost]
        [Route("VerifyOtp")]

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult VerifyOtp([FromBody] VerifyOTP request)
        {
            try
            {
                if (request.Code.Length > 6 || request.Code.Length < 6)
                {
                    return BadRequest("Verification code is a six digit number");

                }
                Response response = UserService.ValidateOTP(request.Code,request.Email);
                if (response.statusCode == "00")
                {
                    return Ok(response);
                }
                else
                {   
                    return StatusCode(500, response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost]
        [Route("ResetPassword")]

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ResetPassword([FromBody] PasswordResetRequest request)
        {
            try
            {
                LoginUserRequest user = new LoginUserRequest() { Email = request.Email, Password = request.OldPassword };
                Response response1 = UserService.Login(user);
                if (response1.statusCode == "00")
                {
                    PasswordForgotRequest somePass = new PasswordForgotRequest() { Email=request.Email, Password=request.NewPassword};
                    Response response = UserService.ForgotPassword(somePass);
                    if (response.statusCode == "00")
                    {
                        return Ok(response);
                    }
                    else
                    {
                        return StatusCode(500, response);
                    }
                }
                else
                {
                    response1.status = "Invalid Old Password";
                    response1.statusCode = "01";
                    return StatusCode(500, response1);

                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        /*

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}

   