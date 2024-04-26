using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V_Tube.Application.Abstractions.IRepository;
using V_Tube.Application.DTO;
using V_Tube.Domain.Models;
using V_Tube.Persisitence.DataBase;
using V_Tube.Persisitence.LinqMethods;

namespace V_Tube.Persisitence.Repositories
{
    public class PlaylistRepository : BaseRepository<PlayList>, IPlaylistRepository
    {
        public PlaylistRepository(VTubeDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<PlaylistResponse>> GetAllPlaylists(Guid channelId)
        {
            string query = $@"SELECT Id,ChannelId,[Name],[Description],
                            (SELECT COUNT(Id) FROM Videos WHERE PlayListId = P.ID) AS NoOfVideos,
                            (SELECT [Name] FROM Channels WHERE Id = P.ChannelId) AS Channel,
                            (SELECT TOP 1 Thumbnail FROM Videos WHERE PlayListId = P.Id ORDER BY CreatedAt) AS Thumbnail
                            FROM PlayLists P
                            WHERE ChannelId = @channelid ";

            return await dbContext.QueryAsync<PlaylistResponse>(query, new { channelId });
        }
    }
}
