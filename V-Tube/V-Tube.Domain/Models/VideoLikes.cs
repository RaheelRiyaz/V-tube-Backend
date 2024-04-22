using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V_Tube.Domain.Models
{
    public class VideoLikes : BaseEntity
    {
        public Guid VideoId { get; set; }
        public Guid UserId { get; set; }
        public bool IsLiked { get; set; } 







        #region Navigational Properties
       /* [ForeignKey(nameof(VideoId))]
        public Video Video { get; set; } = null!;*/

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;
        #endregion Navigational Properties
    }
}
