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
                return BadRequest("phone number and email invalid");
            }
            Response response = UserService.Create(customer);
            return Ok(response);

        }
        [HttpPost]
        [Route("Login")]

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Login([FromBody] LoginUserRequest cred)
        {
            Response response = UserService.Login(cred);
            return Ok(response);


        }
        [HttpPost]
        [Route("ResendOTP")]

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ResendOTP([FromBody] Email email )
        {
            Response response = UserService.ResendOTP(email.UserEmail);
            return Ok(response);


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
                return BadRequest("phone number and email invalid");
            }
            if (cred.Code.Length > 6 || cred.Code.Length < 6)
            {
                return BadRequest("Verification code is a six digit number");

            }
            Response response = UserService.ValidateEmail(cred);
            return Ok(response);

        }
        [HttpPost]
        [Route("VerifyAccount")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult VerifyAccount([FromBody] Email email)
        {

            if (!UtilityService.IsValidEmail(email.UserEmail))
            {
                return BadRequest("phone number and email invalid");
            }
            
            Response response = UserService.ValidateAccount(email.UserEmail);
            return Ok(response);

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
                return BadRequest("phone number and email invalid");
            }

            Response response = UserService.UpdateAccount(request);
            return Ok(response);

        }
        [HttpDelete]
        [Route("DeleteAccount")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteAccount([FromBody] Email email)
        {

            if (!UtilityService.IsValidEmail(email.UserEmail))
            {
                return BadRequest("phone number and email invalid");
            }

            Response response = UserService.DeleteAccount(email.UserEmail);
            return Ok(response);

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

   