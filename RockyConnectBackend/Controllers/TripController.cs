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
        [HttpPut]
        [Route("UpdateATrip")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateTrip([FromBody] UserRequest customer)
        {

            if (!UtilityService.IsPhoneNbr(customer.PhoneNumber) && !UtilityService.IsValidEmail(customer.Email))
            {
                return BadRequest("phone number and email invalid");
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
        [HttpGet]
        [Route("SelectATrip")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SelectATrip([FromBody] TripRequest trip)
        {

            if (!UtilityService.IsValidEmail(trip.Email))
            {
                return BadRequest("email invalid");
            }
            try
            {
                Response response = TripService.GetTrip(trip);
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
        [Route("SelectTripHistory")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult TripHistory([FromBody] TripRequest trip)
        {

            if (!UtilityService.IsValidEmail(trip.Email))
            {
                return BadRequest("email invalid");
            }
            try
            {
                Response response = TripService.GetTripHistory(trip);
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
        [Route("DeleteATrip")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteTrip([FromBody] UserRequest customer)
        {

            if (!UtilityService.IsPhoneNbr(customer.PhoneNumber) && !UtilityService.IsValidEmail(customer.Email))
            {
                return BadRequest("phone number and email invalid");
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
    }
}

