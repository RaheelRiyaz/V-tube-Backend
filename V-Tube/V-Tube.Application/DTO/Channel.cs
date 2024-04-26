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

    public class ChannelResponseForUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Handle { get; set; } = null!;
        public string ProfileUrl { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int HasSubscribed { get; set; }
        public int Subscribers { get; set; }
        public int Notified { get; set; }
        public int TotalVideos { get; set; }
    }
}
