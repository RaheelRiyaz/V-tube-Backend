using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using V_Tube.Application.Abstractions.IServices;
using V_Tube.Application.API_Response;
using V_Tube.Application.DTO;

namespace V_Tube.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController (ICommentsService commentsService) : ControllerBase
    {

        [HttpPost]
        public async Task<APIResponse<int>> AddComment(CommentRequest model) =>
            await commentsService.AddComment(model);


        [HttpGet("video/{videoId:guid}")]
        public async Task<APIResponse<IEnumerable<CommentResponse>>> ViewCommentsByVideoId(Guid videoId) =>
            await commentsService.ViewCommentsByVideoId(videoId);


        [HttpPost("reply-comment")]
        public async Task<APIResponse<int>> AddReplyToComment(ReplyCommentRequest model) =>
            await commentsService.AddReplyToComment(model);


        [HttpGet("replies/{commentId:guid}")]
        public async Task<APIResponse<IEnumerable<CommentReplyResponse>>> FetchCommentReplies(Guid commentId) =>
            await commentsService.FetchCommentReplies(commentId);
    }
}
