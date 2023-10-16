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
    [Route("/")]
    public class PaymentController : Controller
    {

        [HttpPost]
        [Route("AddNewCard")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateCard([FromBody] PaymentCard customer)
        {
            if(customer.Email is not null)
            if (!UtilityService.IsValidEmail(customer.Email))
            {
                return BadRequest("email invalid");
            }
            try
            {
                Response response = PaymentService.CreateCard(customer);
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
        [Route("PaymentCardList")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SavedPaymentCard([FromBody] SavedCardsRequest customer)
        {

            if (!UtilityService.IsValidEmail(customer.Email))
            {
                return BadRequest("phone number and email invalid");
            }
            try
            {
                Response response = PaymentService.GetPaymentCardList(customer);
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
        [Route("GetPaymentCard")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetPaymentCard([FromBody] SavedCardRequest customer)
        {
            if (customer.Email is not null)
                if (!UtilityService.IsValidEmail(customer.Email))
            {
                return BadRequest("email invalid");
            }
            try
            {
                Response response = PaymentService.GetPaymentCard(customer);
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
        [Route("UpdatePaymentCard")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePaymentCard([FromBody] CardUpdate customer)
        {

            if (!UtilityService.IsValidEmail(customer.Email))
            {
                return BadRequest("email invalid");
            }
            try
            {
                Response response = PaymentService.UpdatePaymentCard(customer);
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
        [Route("DeletePaymentCard")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeletePaymentCard([FromBody] SavedCardRequest customer)
        {
            if (customer.Email is not null)
                if (!UtilityService.IsValidEmail(customer.Email))
                 {
                return BadRequest("email invalid");
                 }
            try
            {
                Response response = PaymentService.DeletePaymentCard(customer);
                if (response.statusCode == "00")
                {
                    return Ok(response);
                }
                else
                {
                    return StatusCode(404, response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [HttpPost]
        [Route("MakePayment")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult MakePayment([FromBody] PaymentRequest card)
        {

            if (!!UtilityService.IsValidEmail(card.DrivOwnEmail) && !UtilityService.IsValidEmail(card.RidRentEmail))
            {
                return BadRequest("phone number and email invalid");
            }
            Response response = PaymentService.MakePayment(card);
            return Ok(response);

        }
        // [HttpGet]
        //        [Route("GetTransactionRecordListCustomer")]
        //        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        //        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //        public IActionResult MakePayment([FromBody] UserRequest customer)
        //        {

        //            if (!UtilityService.IsPhoneNbr(customer.PhoneNumber) && !UtilityService.IsValidEmail(customer.Email))
        //            {
        //                return BadRequest("phone number and email invalid");
        //            }
        //            Response response = UserService.Create(customer);
        //            return Ok(response);

        //        }
        //        [HttpGet]
        //        [Route("GetTransactionRecordListRider")]
        //        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        //        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //        public IActionResult MakePayment([FromBody] UserRequest customer)
        //        {

        //            if (!UtilityService.IsPhoneNbr(customer.PhoneNumber) && !UtilityService.IsValidEmail(customer.Email))
        //            {
        //                return BadRequest("phone number and email invalid");
        //            }
        //            Response response = UserService.Create(customer);
        //            return Ok(response);

        //        }
        //// POST api/values
        //[HttpPost]
        //[Route("MakePayment")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public IActionResult MakePayment([FromBody] UserRequest customer)
        //{

        //    if (!UtilityService.IsPhoneNbr(customer.PhoneNumber) && !UtilityService.IsValidEmail(customer.Email))
        //    {
        //        return BadRequest("phone number and email invalid");
        //    }
        //    Response response = UserService.Create(customer);
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

}