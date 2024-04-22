using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V_Tube.Domain.Models
{
    public class Comment : BaseEntity
    {
        public Guid EntityId { get; set; }
        public string Message { get; set; } = null!;
        public bool IsLiked { get; set; }
    }
}
