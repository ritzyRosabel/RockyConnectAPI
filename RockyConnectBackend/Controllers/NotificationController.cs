using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RockyConnectBackend.Model;

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
        [Route("RegisterCar")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
       
            public async Task<IActionResult> SendNotification(NotificationModel notificationModel)
            {
                var result = await _notificationService.SendNotification(notificationModel);
                return Ok(result);
            }
        }
    }
