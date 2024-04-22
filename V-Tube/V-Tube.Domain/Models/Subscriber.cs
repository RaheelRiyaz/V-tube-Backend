using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V_Tube.Domain.Models
{
    public class Subscriber : BaseEntity
    {
        public Guid ChannelId { get; set; }
        public Guid UserId { get; set; }
        public bool Notify { get; set; } = false;



        #region Navigational Properties
        [ForeignKey(nameof(ChannelId))]
        public Channel Channel { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;
        #endregion Navigational Properties

    }
}
