using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V_Tube.Domain.Models
{
    public class PlayList : BaseEntity
    {
        public Guid ChannelId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;



        #region Navigational Properties
        [ForeignKey(nameof(ChannelId))]
        public Channel Channel { get; set; } = null!;
        #endregion Navigational Properties
    }
}
