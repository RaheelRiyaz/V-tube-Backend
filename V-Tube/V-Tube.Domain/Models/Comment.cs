using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V_Tube.Domain.Models
{
    public class Comment : BaseEntity
    {
        public Guid CommentedBy { get; set; }
        public Guid EntityId { get; set; }
        public string Message { get; set; } = null!;



        #region Navigational Properties
        [ForeignKey(nameof(CommentedBy))]
        public User User { get; set; } = null!;
        #endregion Navigational Properties
    }
}
