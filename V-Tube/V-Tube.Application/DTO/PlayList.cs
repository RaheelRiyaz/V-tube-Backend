using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V_Tube.Application.DTO
{
   public record PlaylistRequest
        (
       Guid ChannelId,
       string Name,
       string Description
       );

    public record AddVideoToPlaylistRequest
        (
        Guid VideoId,
        Guid PlaylistId
        );
    public class PlaylistResponse
    {
        public Guid Id { get; set; }
        public Guid ChannelId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Thumbnail { get; set; } = null!;
        public string Channel { get; set; } = null!;
        public int NoOfVideos { get; set; }
    }
}
