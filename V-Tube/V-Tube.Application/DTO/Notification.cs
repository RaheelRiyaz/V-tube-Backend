using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V_Tube.Application.DTO
{
    public class NotificationResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ChannelId { get; set; }
        public string Title { get; set; } = null!;
        public string Channel { get; set; } = null!;
        public string ProfileUrl { get; set; } = null!;
        public string PostedAt { get; set; } = null!;
        public bool HasRead { get; set; }
    }


    public record FetchNotificationRequest
        (
        int PageNo,
        int PageSize
        );

    public class NotificationRequest
    {
        public string Title { get; set; } = null!;
        public Guid ChannelId { get; set; }
        public Guid? VideoId { get; set; }
        public Guid? CommentId { get; set; }
        public Guid UserId { get; set; }
        public Guid? SubscribedBy { get; set; }
    }
}
