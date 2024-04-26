using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V_Tube.Domain.Enums
{
    public enum NotificationType : byte
    {
        Subscribtion = 1,
        VideoAdded = 2,
        VideoLike = 3,
        CommentLike = 4,
        Views = 5,
        Commented = 6,
        CommentReply = 7 
    }

    public enum LikeType : byte
    {
        Video = 1,
        Comment = 2,
    }
}
