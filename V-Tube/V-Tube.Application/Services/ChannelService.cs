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
    public class ChannelService
        (
        IChannelRepository repository,
        ICloudinaryService cloudinaryService,
        IContextService contextService,
        ISubsribeRepository subsribeRepository,
        INotificationsRepository notificationsRepository
        ) : IChannelService
    {
        public async Task<APIResponse<int>> CreateChannel(ChannelRequest model)
        {
            if (string.IsNullOrEmpty(model.Name.Trim()) || string.IsNullOrEmpty(model.Handle.Trim()) || string.IsNullOrEmpty(model.Description.Trim()))
                return APIResponse<int>.ErrorResponse("Name or handle is invalid");

            var userId = Guid.Parse("861B6371-426C-4868-ACD7-F96DBE227456");

            if (userId == Guid.Empty)
                return APIResponse<int>.ErrorResponse("Invalid user");
            //var userId = contextService.GetContextId();

            var channelExists = await repository.ExistsAsync(_ => _.Id == userId);

            if (channelExists)
                return APIResponse<int>.ErrorResponse("Already have a channel");

            var CLOUDINARY_RESPONSE = await cloudinaryService.UploadFileOnCloudinary(model.File);

            if (CLOUDINARY_RESPONSE is null)
                return APIResponse<int>.ErrorResponse("Couldn't upload file");

            var chnnael = new Channel
            {
                Id = userId,
                Handle = model.Handle,
                Name = model.Name,
                Description = model.Description,
                ProfileUrl = CLOUDINARY_RESPONSE.Url.ToString()
            };


            var res = await repository.InsertAsync(chnnael);


            if (res > 0)
                return APIResponse<int>.SuccessResponse(res, "Channel has been created");

            return APIResponse<int>.ErrorResponse();
        }

        public async Task<APIResponse<int>> NotifyChannel(Guid channelId)
        {
            var channel = await repository.FindOneAsync(channelId);

            if (channel is null)
                return APIResponse<int>.ErrorResponse("No Such Channel Found");

            //var user = contextService.GetContextId();
            var user = Guid.Parse("7D76CA0F-A8F6-49B6-9226-6CC05D8E3E4B");

            if (user == Guid.Empty)
                return APIResponse<int>.ErrorResponse("Invalid user");

            if (channel.Id == user)
                return APIResponse<int>.ErrorResponse("Channel owner cannot have this option");

            var subscriber = await subsribeRepository.FirstOrDefaultAsync(_ => _.UserId == user && _.ChannelId == channelId);

            if(subscriber is null)
                return APIResponse<int>.ErrorResponse("You haven't Subscribed this VTube Channel");

            subscriber.Notify = !subscriber.Notify;
            subscriber.UpdatedAt = DateTime.Now;

            var res = await subsribeRepository.UpdateAsync(subscriber);

            if (res > 0)
                return APIResponse<int>.SuccessResponse(res, "You have toggled the noify reminder");

            return APIResponse<int>.ErrorResponse();
        }

        public async Task<APIResponse<int>> SubscribeUnsubscribeChannel(Guid channelId)
        {
            var channel = await repository.FindOneAsync(channelId);

            if (channel is null)
                return APIResponse<int>.ErrorResponse("No Such Channel Found");

            // var user = contextService.GetContextId();
            var user = Guid.Parse("7D76CA0F-A8F6-49B6-9226-6CC05D8E3E4B");

            if(user == Guid.Empty)
                return APIResponse<int>.ErrorResponse("Invalid user");

            if (channel.Id == user)
                return APIResponse<int>.ErrorResponse("Channel owner cannot subscribe or unsubscribe");

            var subscriber = await subsribeRepository.FirstOrDefaultAsync(_ => _.UserId == user && _.ChannelId == channelId);

            if(subscriber is null)
            {
                var newSubscriber = new Subscriber
                {
                    ChannelId = channelId,
                    UserId = user,
                };

                var result = await subsribeRepository.InsertAsync(newSubscriber);
               
                if (result > 0)
                    return APIResponse<int>.SuccessResponse(result, "You have Subscribed this VTube Channel");

                return APIResponse<int>.ErrorResponse();
            }

            var res = await subsribeRepository.DeleteAsync(subscriber);

            if(res > 0)
                return APIResponse<int>.SuccessResponse(res, "You have Unsubscribed this VTube Channel");

            return APIResponse<int>.ErrorResponse();
        }

        public async Task<APIResponse<IEnumerable<ChannelResponseForUser>>> ViewChannels()
        {
            var userId = Guid.Parse("861B6371-426C-4868-ACD7-F96DBE227456");

            if (userId == Guid.Empty)
                return APIResponse<IEnumerable<ChannelResponseForUser>>.ErrorResponse("Invalid user");

            //var userId = contextService.GetContextId();

            var channels = await repository.ViewChannels(userId);

            return APIResponse<IEnumerable<ChannelResponseForUser>>.SuccessResponse(channels, "Here is list of channels");

        }
    }
}
