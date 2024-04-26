using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V_Tube.Application.Abstractions.IRepository;
using V_Tube.Application.Abstractions.IServices;
using V_Tube.Application.API_Response;
using V_Tube.Application.DTO;
using V_Tube.Domain.Models;

namespace V_Tube.Application.Services
{
    public class CommentsService 
        (
        ICommentsRepository commentsRepository,
        ICommentRepliesRepository commentRepliesRepository,
        IContextService contextService
        ) : ICommentsService
    {
        public async Task<APIResponse<int>> AddComment(CommentRequest model)
        {
            //var userId = contextService.GetContextId();
            var userId = Guid.Parse("861B6371-426C-4868-ACD7-F96DBE227456");

            var comment = new Comment
            {
                CommentedBy = userId,
                EntityId = model.VideoId,
                Message = model.Message,
            };

            var res = await commentsRepository.InsertAsync(comment);

            return res > 0 ? APIResponse<int>.SuccessResponse(res, "You have commented to this video")
                : APIResponse<int>.ErrorResponse("Error while commenting");
        }

        public async Task<APIResponse<int>> AddReplyToComment(ReplyCommentRequest model)
        {
            //var userId = contextService.GetContextId();
            var userId = Guid.Parse("861B6371-426C-4868-ACD7-F96DBE227456");
            var comment = await commentsRepository.FindOneAsync(model.CommentId);

            if (comment is null)
                return APIResponse<int>.ErrorResponse("Invalid Comment");

            var reply = new CommentReply
            {
                CommentId = model.CommentId,
                Message = model.Message,
                RepliedBy = userId
            };

            var result = await commentRepliesRepository.InsertAsync(reply);

            return result > 0 ?
                APIResponse<int>.SuccessResponse(result, "You replies to this comment"):
                APIResponse<int>.ErrorResponse();
        }

        public async Task<APIResponse<IEnumerable<CommentResponse>>> ViewCommentsByVideoId(Guid videoId)
        {
            //var userId = contextService.GetContextId();
            var userId = Guid.Parse("861B6371-426C-4868-ACD7-F96DBE227456");

            var comments = await commentsRepository.FetchVideoComments(videoId,userId);

            return APIResponse<IEnumerable<CommentResponse>>.SuccessResponse(comments, "Comments fetched successfully");
        }
    }
}
