
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V_Tube.Application.API_Response;
using V_Tube.Application.DTO;

namespace V_Tube.Application.Abstractions.IServices
{
    public interface IChannelService
    {
        Task<APIResponse<int>> CreateChannel(ChannelRequest model);

        Task<APIResponse<int>> SubscribeUnsubscribeChannel(Guid channelId);

        Task<APIResponse<int>> NotifyChannel(Guid channelId);
        Task<APIResponse<IEnumerable<ChannelResponseForUser>>> ViewChannels();
    }
}
