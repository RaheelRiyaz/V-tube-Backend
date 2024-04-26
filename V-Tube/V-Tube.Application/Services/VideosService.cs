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
    public class VideosService
        (
        IVideosRepository repository,
        IChannelRepository channelRepository,
        ICloudinaryService cloudinaryService,
        IContextService contextService,
        ISubsribeRepository subsribeRepository
        ) : IVideosService
    {
        public async Task<APIResponse<IEnumerable<VideoDisplayResponse>>> FetchDisplayVideos(VideoFilter model)
        {
            //var userId = contextService.GetContextId();
            var userId = Guid.Parse("861B6371-426C-4868-ACD7-F96DBE227456");

            var hasUserSubscribedToAnyChannel = await subsribeRepository.ExistsAsync(_=>_.UserId == userId);

            var videos = await repository.GetDisplayVideos(model, userId,hasUserSubscribedToAnyChannel);

            return APIResponse<IEnumerable<VideoDisplayResponse>>.SuccessResponse(videos);
        }

        public async Task<APIResponse<int>> UploadVideo(VideoRequest model)
        {
            if(string.IsNullOrEmpty(model.Title.Trim()) || string.IsNullOrEmpty(model.Description.Trim()))
                    return APIResponse<int>.ErrorResponse("Video title or description is invalid");

            var channelExists = await channelRepository.ExistsAsync(_ => _.Id == model.ChannelId);

            if (!channelExists)
                return APIResponse<int>.ErrorResponse("Channel is not found");


            var (CLODUINARY_Video_RESPONSE,duration) = await cloudinaryService.UploadVideoFileOnCloudinary(model.Video);

            if (CLODUINARY_Video_RESPONSE is null)
                return APIResponse<int>.ErrorResponse("Couldn't upload video please try again later");

            var CLODUINARY_Thumbnail_RESPONSE = await cloudinaryService.UploadFileOnCloudinary(model.Thumbnail);

            if (CLODUINARY_Thumbnail_RESPONSE is null)
                return APIResponse<int>.ErrorResponse("Couldn't upload thumbnail please try again later");

            var video = new SPVideoUploadRequest
                (
                model.ChannelId,
                model.PlayListId ?? null,
                model.Title,
                model.Description,
                Convert.ToString(CLODUINARY_Thumbnail_RESPONSE.Url)!,
                0,
                Convert.ToString(CLODUINARY_Video_RESPONSE.Url)!
                );

            var result = await repository.UploadVideoAndNotifySubscribers(video);

            if (result > 0)
                return APIResponse<int>.SuccessResponse(result, "Video has been uploaded succesfully");

            return APIResponse<int>.ErrorResponse();
        }
    }
}
