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
    //[Authorize]
    [Route("/")]
    public class PaymentController : Controller
    {
        [HttpPost]
        [Route("RegisterBankDetail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateBank([FromBody] Bank bank)
        {
            if (!UtilityService.IsValidEmail(bank.Email))
            {
                return BadRequest("email invalid");
            }
            try
            {
                Response response = BankService.CreateBank(bank);
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
        [Route("GetBankDetail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetBank(string email)
        {
            if (email is not null)
                if (!UtilityService.IsValidEmail(email))
                {
                    return BadRequest("email invalid");
                }
            try
            {
                Response response = BankService.GetBank(email);
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
        [Route("UpdateBankDetail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateBank([FromBody] Bank bank)
        {

            if (!UtilityService.IsValidEmail(bank.Email))
            {
                return BadRequest("email invalid");
            }
            try
            {
                Response response = BankService.UpdateBank(bank);
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
        [Route("AddNewCard")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateCard([FromBody]PaymentCardRequest customer)
        {
            if (!UtilityService.IsValidEmail(customer.Email)|| customer.Pan.Length!=16)
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
        public IActionResult SavedPaymentCard(SavedCardsRequest customer)
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
        public IActionResult GetPaymentCard( SavedCardRequest customer)
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
        [Route("PaymentReminder")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PaymentReminder(string email, string tripId)
        {
            if (email is not null)
                if (!UtilityService.IsValidEmail(email))
                 {
                return BadRequest("email invalid");
                 }
            try
            {
                Response response = PaymentService.PaymentReminder(email, tripId);
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

            if (!UtilityService.IsValidEmail(card.DrivOwnEmail) || !UtilityService.IsValidEmail(card.RidRentEmail))
            {
                return BadRequest("phone number and email invalid");
            }
            if (!card.SavedCard)
            {
                if (card.Card.Pan == "string" || card.Card.Pan.Length != 16)
                {
                    return BadRequest("incorrect card details, Please try again");
                }
            } if (card.SavedCard)
            {
                if (card.CardAlias==string.Empty||card.CardAlias=="string"||card.CardAlias is null)
                {
                    return BadRequest("incorrect card Alais, Please try again");
                }
            }
            
            Response response = PaymentService.MakePayment(card);
            if (response.statusCode == "00")
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(500, response);
            }
        }

       
    }
  
}

