using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V_Tube.Domain.Models
{
    public class CommentReply : BaseEntity
    {
        public Guid CommentId { get; set; }
        public string Message { get; set; } = null!;
        public bool IsLiked { get; set; }




        #region Navigational Properties
        [ForeignKey(nameof(CommentId))]
        public Comment Comment { get; set; } = null!;
        #endregion Navigational Properties
    }
}
