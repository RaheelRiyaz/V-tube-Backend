using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V_Tube.Application.Abstractions.IRepository;
using V_Tube.Application.Abstractions.IServices;
using V_Tube.Application.API_Response;
using V_Tube.Application.DTO;
using V_Tube.Domain.Enums;
using V_Tube.Domain.Models;

namespace V_Tube.Application.Services
{
    public class LikeService(
        ILikesRepository repository,
        IContextService contextService
        ) : ILikeService
    {
        public async Task<APIResponse<int>> AddLike(LikeRequest model)
        {
            //var userId = contextService.GetContextId();
            var userId = Guid.Parse("861B6371-426C-4868-ACD7-F96DBE227456");

            switch (model.LikeType)
            {
                case LikeType.Video:
                    if (model.VideoId is null)
                        return APIResponse<int>.ErrorResponse("Video id is required in this case");

                    var videoLiked = await repository.FirstOrDefaultAsync(_ => _.LikedBy == userId && _.VideoId == model.VideoId);

                    if (videoLiked is null)
                    {
                        var newVideoLike = new Likes
                        {
                            VideoId = model.VideoId,
                            LikedBy = userId,
                            IsLiked = Convert.ToBoolean(model.IsLiked),
                            LikeType = LikeType.Video
                        };

                        var addedVideoLikeresult = await repository.InsertAsync(newVideoLike);

                        return addedVideoLikeresult > 0 ?
                            APIResponse<int>.SuccessResponse(addedVideoLikeresult, "Video Liked successfully") :
                            APIResponse<int>.ErrorResponse();
                    }

                    if (model.IsLiked is null)
                    {
                        var deletedVideoLike = await repository.DeleteAsync(videoLiked);

                        return deletedVideoLike > 0 ?
                        APIResponse<int>.SuccessResponse(deletedVideoLike, "Video response has been removed") :
                        APIResponse<int>.ErrorResponse();
                    }

                    if (videoLiked.IsLiked == model.IsLiked)
                        return APIResponse<int>.ErrorResponse("Already responded to this video");

                    videoLiked.IsLiked = Convert.ToBoolean(model.IsLiked);
                    videoLiked.UpdatedAt = DateTime.Now;
                    var updatedVideoLikeResult = await repository.UpdateAsync(videoLiked);

                    return updatedVideoLikeResult > 0 ?
                        APIResponse<int>.SuccessResponse(updatedVideoLikeResult, "Video response has been updated") :
                        APIResponse<int>.ErrorResponse();


                case LikeType.Comment:
                    if (model.CommentId is null)
                        return APIResponse<int>.ErrorResponse("Comment id is required in this case");

                    var commentLiked = await repository.FirstOrDefaultAsync(_ => _.LikedBy == userId && _.CommentId == model.CommentId);
                    if (commentLiked is null)
                    {
                        var newVideoLike = new Likes
                        {
                            CommentId = model.CommentId,
                            LikedBy = userId,
                            IsLiked = Convert.ToBoolean(model.IsLiked),
                            LikeType = LikeType.Comment
                        };

                        var addedCommentLikeresult = await repository.InsertAsync(newVideoLike);

                        return addedCommentLikeresult > 0 ?
                            APIResponse<int>.SuccessResponse(addedCommentLikeresult, "Comment Liked successfully") :
                            APIResponse<int>.ErrorResponse();
                    }

                    if (model.IsLiked is null)
                    {
                        var deletedCoomentLike = await repository.DeleteAsync(commentLiked);

                        return deletedCoomentLike > 0 ?
                        APIResponse<int>.SuccessResponse(deletedCoomentLike, "Comment response has been removed") :
                        APIResponse<int>.ErrorResponse();
                    }

                    if (commentLiked.IsLiked == model.IsLiked)
                        return APIResponse<int>.ErrorResponse("Already responded to this comment");

                    commentLiked.IsLiked = Convert.ToBoolean(model.IsLiked);
                    commentLiked.UpdatedAt = DateTime.Now;
                    var updatedCommentLikeResult = await repository.UpdateAsync(commentLiked);

                    return updatedCommentLikeResult > 0 ?
                        APIResponse<int>.SuccessResponse(updatedCommentLikeResult, "Comment response has been updated") :
                        APIResponse<int>.ErrorResponse();
            }

            return APIResponse<int>.ErrorResponse();

        }
    }
}
