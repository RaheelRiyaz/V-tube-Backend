using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V_Tube.Application.DTO
{
    public record CommentRequest
        (
        Guid VideoId,
        string Message
        );

    public record CommentLikeRequest
        (
        Guid CommentId,
        bool? IsLiked
        );


    public class CommentResponse
    {
        public Guid VideoId { get; set; }
        public Guid Id { get; set; }
        public Guid CommentedBy { get; set; }
        public string TimeAgo { get; set; } = null!;
        public string Message { get; set; } = null!;
        public string Commenter { get; set; } = null!;
        public int Replies { get; set; }
        public bool? HasUserLiked { get; set; }
    }

    public record ReplyCommentRequest
        (
        Guid CommentId,
        string Message
        );

    public class CommentReplyResponse
    {
        public Guid Id { get; set; }
        public Guid CommentId { get; set; }
        public Guid RepliedBy { get; set; }
        public string TimeAgo { get; set; } = null!;
        public string Message { get; set; } = null!;
        public string Replier { get; set; } = null!;
        public bool? HasUserLiked { get; set; }
    }
}
