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
    public class ChannelRepository : BaseRepository<Channel>, IChannelRepository
    {
        public ChannelRepository(VTubeDbContext dbContext) : base(dbContext)
        {
        }

        public Task<IEnumerable<ChannelResponseForUser>> ViewChannels(Guid userId)
        {
            string query = $@"SELECT * ,
                            ISNULL((SELECT 1 FROM Subscribers WHERE ChannelId = C.Id AND UserId = @userId),0)
                            AS HasSubscribed,
                            (SELECT COUNT(Id) FROM Subscribers WHERE ChannelId = C.Id) AS Subscribers, 
                            ISNULL((SELECT Notify FROM Subscribers WHERE ChannelId = C.Id AND UserId = @userId),0)
                            AS Notifed,
                            (SELECT COUNT(Id) FROM Videos WHERE ChannelId = C.Id) AS TotalVideos
                            FROM Channels C ";

            return dbContext.QueryAsync<ChannelResponseForUser>(query, new { userId });
        }
    }
}
