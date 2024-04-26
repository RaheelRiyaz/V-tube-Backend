using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V_Tube.Domain.Models
{
    public class Video : BaseEntity
    {
        public Guid ChannelId { get; set; }
        public Guid? PlayListId { get; set; }
        public string Url { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Thumbnail { get; set; } = null!;
        public bool IsPublished { get; set; } = true;
        public int Duration { get; set; }



        #region Navigational Properties
        [ForeignKey(nameof(ChannelId))]
        public Channel Channel { get; set; } = null!;
        [ForeignKey(nameof(PlayListId))]
        public PlayList PlayList { get; set; } = null!;
        #endregion Navigational Properties
    }
}
