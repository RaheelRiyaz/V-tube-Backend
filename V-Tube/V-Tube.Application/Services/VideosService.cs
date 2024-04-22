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
        ICloudinaryService cloudinaryService
        ): IVideosService
    {
        public async Task<APIResponse<int>> UploadVideo(VideoRequest model)
        {
            var channelExists = await channelRepository.ExistsAsync(_ => _.Id == model.ChannelId);

            if (!channelExists)
                return APIResponse<int>.ErrorResponse("Channel is not found");

            var CLODUINARY_Thumbnail_RESPONSE = await cloudinaryService.UploadFileOnCloudinary(model.Thumbnail);

            var CLODUINARY_Video_RESPONSE = await cloudinaryService.UploadFileOnCloudinary(model.Video);

            if (CLODUINARY_Thumbnail_RESPONSE is null || CLODUINARY_Video_RESPONSE is null)
                return APIResponse<int>.ErrorResponse("Couldn't upload file please try again later");

            var video = new Video 
            {
                ChannelId = model.ChannelId,
                Description = model.Description,
                Title = model.Title,
                Thumbnail = Convert.ToString(CLODUINARY_Thumbnail_RESPONSE.Url)!,
                Url = Convert.ToString(CLODUINARY_Video_RESPONSE.Url)!,
                Duration = Convert.ToDouble(CLODUINARY_Video_RESPONSE.Bytes),
                PlayListId = model.PlayListId ?? null,
            };

            var result = await repository.InsertAsync(video);

            if(result > 0)
            return APIResponse<int>.SuccessResponse(result,"Video has been uploaded succesfully");

            return APIResponse<int>.ErrorResponse();
        }
    }
}
