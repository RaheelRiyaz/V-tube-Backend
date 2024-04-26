using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V_Tube.Application.Abstractions.IRepository;
using V_Tube.Application.Abstractions.IServices;
using V_Tube.Application.API_Response;
using V_Tube.Application.DTO;

namespace V_Tube.Application.Services
{
    public class NotificationsService
        (
        INotificationsRepository repository,
        IContextService contextService
        ) : INotificationsService
    {
        public async Task<APIResponse<IEnumerable<NotificationResponse>>> FetchUserNotifications(FetchNotificationRequest model)
        {
            //var userId = contextService.GetContextId();
            var userId = Guid.Parse("7D76CA0F-A8F6-49B6-9226-6CC05D8E3E4B");

            var notifications = await repository.FetchUserNotifications(model,userId);

            return APIResponse<IEnumerable<NotificationResponse>>.SuccessResponse(notifications, "User notifications");
        }

        public async Task<APIResponse<int>> MarkNotificationAsRead(Guid id)
        {
            var notification = await repository.FindOneAsync(id);

            if (notification is null)
                return APIResponse<int>.ErrorResponse("No notificiation found with such id");

            notification.HasRead = true;

            var result = await repository.UpdateAsync(notification);

            if (result > 0)
                return APIResponse<int>.SuccessResponse(result, "Notification marked as read");

            return APIResponse<int>.ErrorResponse();
        }
    }
}
