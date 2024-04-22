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
    public class PlaylistService (
        IPlaylistRepository repository,
        IChannelRepository channelRepository
        ) : IPlaylistService
    {
        public async Task<APIResponse<int>> CreatePlayList(PlaylistRequest model)
        {
            if(string.IsNullOrEmpty(model.Name.Trim()) || string.IsNullOrEmpty(model.Description.Trim()))
                return APIResponse<int>.ErrorResponse("Playlist name or description in invalid");

            var channelExists = await channelRepository.ExistsAsync(_=>_.Id == model.ChannelId);

            if (!channelExists)
                return APIResponse<int>.ErrorResponse("No Such channel created");

            var playList = new PlayList
            {
                ChannelId = model.ChannelId,
                Description = model.Description,
                Name = model.Name,
            };

            var response = await repository.InsertAsync(playList);

            if (response > 0)
                return APIResponse<int>.SuccessResponse(response, "Playlist has been created successfully");

            return APIResponse<int>.ErrorResponse();
        }

        public async Task<APIResponse<int>> AddVideoToPlaylist(AddVideoToPlaylistRequest model)
        {
            var playListExists = await repository.ExistsAsync(_ => _.Id == model.PlaylistId);

            if (!playListExists)
                return APIResponse<int>.ErrorResponse("No such playlist found");

            return APIResponse<int>.SuccessResponse(1);
        }
    }
}
