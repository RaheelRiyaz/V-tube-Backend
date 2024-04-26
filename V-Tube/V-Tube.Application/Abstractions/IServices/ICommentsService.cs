
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V_Tube.Application.API_Response;
using V_Tube.Application.DTO;
using V_Tube.Domain.Models;

namespace V_Tube.Application.Abstractions.IServices
{
    public interface ICommentsService
    {
        Task<APIResponse<int>> AddComment(CommentRequest model);
        Task<APIResponse<int>> AddReplyToComment(ReplyCommentRequest model);
        Task<APIResponse<IEnumerable<CommentResponse>>> ViewCommentsByVideoId(Guid videoId);
    }
}
