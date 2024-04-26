
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
    public class NotificationsRepository : BaseRepository<Notification>, INotificationsRepository
    {
        public NotificationsRepository(VTubeDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<NotificationResponse>> FetchUserNotifications(FetchNotificationRequest model, Guid userId)
        {
            string query = $@"SELECT N.Id,Title,ChannelId,N.UserId,
                            CASE 
                            WHEN DATEDIFF(HOUR,N.CreatedAt,GETDATE()) < 24
                            THEN CAST(DATEDIFF(HOUR,N.CreatedAt,GETDATE()) AS nvarchar(100)) +' hour ago'
                            WHEN DATEDIFF(HOUR,N.CreatedAt,GETDATE()) >= 24 AND DATEDIFF(HOUR,N.CreatedAt,GETDATE()) <= 48
                            THEN CAST(DATEDIFF(DAY,N.CreatedAt,GETDATE()) AS nvarchar(100)) +' day ago'
                            ELSE CAST(N.CreatedAt AS nvarchar(100))
                            END
                            AS PostedAt,
                            HasRead,C.[Name] AS Channel,
                            ProfileUrl
                            FROM Notifications N
                            LEFT JOIN Channels C ON 
                            N.ChannelId = C.Id
                            WHERE UserId = @userId
                            ORDER BY N.CreatedAt DESC
                            OFFSET ((@pageNo - 1) * @pageSize) ROWS
                            FETCH NEXT @pageSize ROWS ONLY ";

            return await dbContext.QueryAsync<NotificationResponse>(query, new {model.PageNo, model.PageSize, userId});
        }

        public Task<int> InsertNotification(Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}
