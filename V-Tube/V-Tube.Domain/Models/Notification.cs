using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V_Tube.Domain.Models
{
    public class Notification : BaseEntity
    {
        public string Title { get; set; } = null!;
        public Guid ChannelId { get; set; }
        public Guid? VideoId { get; set; }
        public Guid? CommentId { get; set; }
        public Guid UserId { get; set; }
        public Guid? SubscribedBy { get; set; }
        public Guid? LikedBy { get; set; }
        public bool HasRead { get; set; } = false;







        #region Navigational Properties
        [ForeignKey(nameof(ChannelId))]
        public Channel Channel { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        [ForeignKey(nameof(SubscribedBy))]
        public User UserSubscribed { get; set; } = null!;

        [ForeignKey(nameof(LikedBy))]
        public User Likes { get; set; } = null!;
        #endregion Navigational Properties

    }
}
