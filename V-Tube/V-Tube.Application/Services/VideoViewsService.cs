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
    public class VideoViewsService
        (
        IVideoViewsRepository repository,
        IVideosRepository videosRepository,
        IContextService contextService,
        IChannelRepository channelRepository
        ) : IVideoViewService
    {
        public async Task<APIResponse<int>> ViewVideo(VideoViewRequest model)
        {
            //var userId = contextService.GetContextId();
            var userId = Guid.Parse("861B6371-426C-4868-ACD7-F96DBE227456");

            var video = await videosRepository.FindOneAsync(model.VideoId);

            if (video is null)
                return APIResponse<int>.ErrorResponse("Invalid video");

            var owner = await channelRepository.FindOneAsync(video.ChannelId);

            if (owner?.Id == userId)
                return APIResponse<int>.ErrorResponse("Viewo owner views cannot be counted");

            var view = await repository.FirstOrDefaultAsync(_ => _.VideoId == model.VideoId && _.VieweById == userId);

            if (view is not null)
            {
                if (view.DurationViewed < model.DurationViewed)
                {
                    view.DurationViewed = model.DurationViewed;
                    var updatedResponse = await repository.UpdateAsync(view);
                    return updatedResponse > 0 ?
                        APIResponse<int>.SuccessResponse(updatedResponse, "You have viewed this video") :
                        APIResponse<int>.ErrorResponse();
                }

                return APIResponse<int>.ErrorResponse("You have already viewed this video");
            }

            var newView = new VideoViews
            {
                DurationViewed = model.DurationViewed,
                VideoId = model.VideoId,
                VieweById = userId
            };
            var addViewResponse = await repository.InsertAsync(newView);

            return addViewResponse > 0 ?
                        APIResponse<int>.SuccessResponse(addViewResponse, "You have viewed this video") :
                        APIResponse<int>.ErrorResponse();

        }
    }
}
