
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V_Tube.Application.API_Response;
using V_Tube.Application.DTO;

namespace V_Tube.Application.Abstractions.IServices
{
    public interface INotificationsService 
    {
        Task<APIResponse<int>> MarkNotificationAsRead(Guid id);
        Task<APIResponse<IEnumerable<NotificationResponse>>> FetchUserNotifications(FetchNotificationRequest model);
    }
}
