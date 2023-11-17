using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RockyConnectBackend.Model;
using RockyConnectBackend.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RockyConnectBackend.Controllers
{
    [Route("api/[controller]")]
    public class NotificationController : Controller
    {
        // GET: api/values
       
            private readonly INotificationService _notificationService;
            public NotificationController(INotificationService notificationService)
            {
                _notificationService = notificationService;
            }

        [HttpPost]
        [Route("SendNotification")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
       
            public async Task<IActionResult> SendNotification(NotificationModel notificationModel)
            {
                var result = await _notificationService.SendNotification(notificationModel);
                return Ok(result);
            }
        [HttpGet]
        [Route("GetNotification")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetNotification(string email)
        {
            if (email is not null)
                if (!UtilityService.IsValidEmail(email))
                {
                    return BadRequest("email invalid");
                }
            try
            {
                Response response = NotificationService.GetNotification(email);
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
