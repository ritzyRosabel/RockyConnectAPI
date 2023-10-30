    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RockyConnectBackend.Model;
    using RockyConnectBackend.Services;

    // For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

    namespace RockyConnectBackend.Controllers
    {

       // [Authorize]
        [Route("/")]
        public class CarController : Controller
        {
            [HttpPost]
            [Route("RegisterCar")]
            [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            public IActionResult CreateCard([FromBody] CarRequest car)
            {
                if (!UtilityService.IsValidEmail(car.Email))
                { 
                    return BadRequest("email invalid");
                }
                try
                {
                    Response response = CarService.CreateCard(car);
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
            [Route("GetCar")]
            [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            public IActionResult GetCar(string email)
            {
                if (email is not null)
                    if (!UtilityService.IsValidEmail(email))
                    {
                        return BadRequest("email invalid");
                    }
                try
                {
                    Response response = CarService.GetCar(email);
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
            [Route("UpdateCar")]
            [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            public IActionResult UpdateCar([FromBody] CarRequest car)
            {

                if (!UtilityService.IsValidEmail(car.Email))
                {
                    return BadRequest("email invalid");
                }
                try
                {
                    Response response = CarService.UpdateCar(car);
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
        // [HttpGet]
        //        [Route("GetTransactionRecordListcar")]
        //        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        //        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //        public IActionResult MakePayment([FromBody] UserRequest car)
        //        {

        //            if (!UtilityService.IsPhoneNbr(car.PhoneNumber) && !UtilityService.IsValidEmail(car.Email))
        //            {
        //                return BadRequest("phone number and email invalid");
        //            }
        //            Response response = UserService.Create(car);
        //            return Ok(response);

        //        }
        //        [HttpGet]
        //        [Route("GetTransactionRecordListRider")]
        //        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        //        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //        public IActionResult MakePayment([FromBody] UserRequest car)
        //        {

        //            if (!UtilityService.IsPhoneNbr(car.PhoneNumber) && !UtilityService.IsValidEmail(car.Email))
        //            {
        //                return BadRequest("phone number and email invalid");
        //            }
        //            Response response = UserService.Create(car);
        //            return Ok(response);

        //        }
        //// POST api/values
        //[HttpPost]
        //[Route("MakePayment")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public IActionResult MakePayment([FromBody] UserRequest car)
        //{

        //    if (!UtilityService.IsPhoneNbr(car.PhoneNumber) && !UtilityService.IsValidEmail(car.Email))
        //    {
        //        return BadRequest("phone number and email invalid");
        //    }
        //    Response response = UserService.Create(car);
        //    return Ok(response);

        //}
        //        // PUT api/values/5
        //        [HttpPut("{id}")]
        //        public void Put(int id, [FromBody]string value)
        //        {
        //        }

        //        // DELETE api/values/5
        //        [HttpDelete("{id}")]
        //        public void Delete(int id)
        //        {
        //        }

    }

