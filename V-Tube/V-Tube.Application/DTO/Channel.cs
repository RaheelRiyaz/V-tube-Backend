using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V_Tube.Application.DTO
{
    public record ChannelRequest
        (
        string Name,
        string Handle,
        string Description,
        IFormFile File
        );

    public record UpdateSubscription
        (
        Guid ChannelId,
        bool Notify
        );
}
