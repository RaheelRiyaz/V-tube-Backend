using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V_Tube.Domain.Enums;

namespace V_Tube.Domain.Models
{
    public class Likes : BaseEntity
    {
        public Guid? VideoId { get; set; }
        public Guid LikedBy { get; set; }
        public bool IsLiked { get; set; } = true;
        public Guid? CommentId { get; set; }
        public LikeType LikeType { get; set; }




        #region Navigational Properties
        [ForeignKey(nameof(LikedBy))]
        public User User { get; set; } = null!;
        #endregion Navigational Properties
    }
}
