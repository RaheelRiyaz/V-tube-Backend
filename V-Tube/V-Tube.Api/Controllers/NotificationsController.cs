using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using V_Tube.Application.Abstractions.IServices;
using V_Tube.Application.API_Response;
using V_Tube.Application.DTO;

namespace V_Tube.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController (INotificationsService service) : ControllerBase
    {
        [HttpGet("markas-read/{id:guid}")]
        public async Task<APIResponse<int>> MarkNotificationAsRead(Guid id) =>
            await service.MarkNotificationAsRead(id);


        [HttpPost("fetch-notifications")]
        public async Task<APIResponse<IEnumerable<NotificationResponse>>> FetchUserNotifications(FetchNotificationRequest model) =>
            await service.FetchUserNotifications(model);
    }
}
