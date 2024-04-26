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


    public record SPVideoUploadRequest
        (
        Guid ChannelId,
        Guid? PlaylistId,
        string Title,
        string Description,
        string Thumbnail,
        int Duration,
        string Url
        );


    public class VideoDisplayResponse
    {
        public Guid VideoId { get; set; }
        public Guid ChannelId { get; set; }
        public Guid? PlayListId { get; set; }
        public string VideoTitle { get; set; } = null!;
        public string VideoDescription { get; set; } = null!;
        public string Thumbnail { get; set; } = null!;
        public string ChannelName { get; set; } = null!;
        public string VideoUrl { get; set; } = null!;
        public string ProfileUrl { get; set; } = null!;
        public string TimeAgo { get; set; } = null!;
        public string TotalViewsFormatted { get; set; } = null!;
        public int Duration { get; set; }
        public int TotalViews { get; set; }
        public int TotalComments { get; set; }
        public int TotalLikes { get; set; }
        public int TotalDislikes { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool HasUserliked { get; set; }
    }


    public record VideoFilter
        (
        Guid? ChannelId,
        Guid? PlayListId,
        int PageNo,
        int PageSize
        );

    public record VideoViewRequest
        (
        Guid VideoId,
        int DurationViewed
        );
}
