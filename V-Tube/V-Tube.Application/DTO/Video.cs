using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V_Tube.Application.DTO
{
   public record VideoRequest
        (
       IFormFile Thumbnail,
       IFormFile Video,
       Guid ChannelId,
       Guid? PlayListId,
       string Title,
       string Description
       );
}
