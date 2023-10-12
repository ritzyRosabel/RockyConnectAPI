using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RockyConnectBackend.Model;
using RockyConnectBackend.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RockyConnectBackend.Controllers
{
    [Route("api/[controller]")]
    public class TripController : Controller
    {
        // GET: api/values
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

        // POST api/values
        [HttpPost]
        [Route("ScheduleATrip")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateTrip([FromBody] UserRequest customer)
        {

            if (!UtilityService.IsPhoneNbr(customer.PhoneNumber) && !UtilityService.IsValidEmail(customer.Email))
            {
                return BadRequest("phone number and email invalid");
            }
            Response response = UserService.Create(customer);
            return Ok(response);

        }
        [HttpPost]
        [Route("CancelATrip")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteTrip([FromBody] UserRequest customer)
        {

            if (!UtilityService.IsPhoneNbr(customer.PhoneNumber) && !UtilityService.IsValidEmail(customer.Email))
            {
                return BadRequest("phone number and email invalid");
            }
            Response response = UserService.Create(customer);
            return Ok(response);

        }
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

