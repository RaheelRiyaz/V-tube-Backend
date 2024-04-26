using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using V_Tube.Application.Abstractions.IServices;
using V_Tube.Application.API_Response;
using V_Tube.Application.DTO;

namespace V_Tube.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelsController (IChannelService service) : ControllerBase
    {
        [HttpPost]
        public async Task<APIResponse<int>> CreateChannel([FromForm] ChannelRequest model) =>
            await service.CreateChannel(model);


        [HttpGet("subscribe-unsubscribe/{channelId:guid}")]
        public async Task<APIResponse<int>> SubscribeUnsubscribeChannel(Guid channelId) =>
            await service.SubscribeUnsubscribeChannel(channelId);


        [HttpGet("toggle-notify/{channelId}")]
        public async Task<APIResponse<int>> NotifyChannel(Guid channelId) =>
            await service.NotifyChannel(channelId);

        [HttpGet]
        public async Task<APIResponse<IEnumerable<ChannelResponseForUser>>> ViewChannels() =>
            await service.ViewChannels();
    }
}
