using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using V_Tube.Application.Abstractions.IServices;
using V_Tube.Application.API_Response;
using V_Tube.Application.DTO;

namespace V_Tube.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistsController(IPlaylistService service) : ControllerBase
    {
        [HttpPost]
        public async Task<APIResponse<int>> CreatePlayList(PlaylistRequest model) =>
            await service.CreatePlayList(model);


        [HttpPost("add-to-playlist")]
        public async Task<APIResponse<int>> CreatePlayList(AddVideoToPlaylistRequest model) =>
            await service.AddVideoToPlaylist(model);


        [HttpGet("channel/{channelId:guid}")]
        public async Task<APIResponse<IEnumerable<PlaylistResponse>>> CheckPlaylists(Guid channelId) =>
            await service.CheckPlaylists(channelId);
    }
}
