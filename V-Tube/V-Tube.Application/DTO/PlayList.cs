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
}
