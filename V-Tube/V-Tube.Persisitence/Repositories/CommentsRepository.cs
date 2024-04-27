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
    public class CommentsRepository : BaseRepository<Comment>, ICommentsRepository
    {
        public CommentsRepository(VTubeDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<CommentResponse>> FetchVideoComments(Guid videoId,Guid userId)
        {
            string query = $@"SELECT C.Id,CommentedBy,
										CASE
                                                    WHEN DATEDIFF(MINUTE, C.CreatedAt, GETDATE()) < 60 THEN CAST(DATEDIFF(MINUTE, C.CreatedAt, GETDATE()) AS NVARCHAR(10)) + ' minute(s) ago'
                                                    WHEN DATEDIFF(HOUR, C.CreatedAt, GETDATE()) < 24 THEN CAST(DATEDIFF(HOUR, C.CreatedAt, GETDATE()) AS NVARCHAR(10)) + ' hour(s) ago'
                                                    WHEN DATEDIFF(DAY, C.CreatedAt, GETDATE()) < 365 THEN CAST(DATEDIFF(DAY, C.CreatedAt, GETDATE()) AS NVARCHAR(10)) + ' day(s) ago'
                                                    ELSE CAST(DATEDIFF(YEAR, C.CreatedAt, GETDATE()) AS NVARCHAR(10)) + ' year(s) ago'
                                                END AS TimeAgo,
										EntityId AS VideoId,
										CASE WHEN
												EXISTS(SELECT Id FROM Likes WHERE CommentId = C.Id AND  LikedBy = @userId)
												THEN (SELECT IsLiked FROM Likes WHERE CommentId = C.Id AND  LikedBy = @userId)
												ELSE NULL
												END AS HasUserLiked,
										(SELECT COUNT(Id) FROM CommentReplies WHERE CommentId = C.Id) AS Replies,
										[Message],UserName AS Commenter
										FROM Comments C
										INNER JOIN Users U
										ON C.CommentedBy = U.Id
										WHERE EntityId = 'CE861B39-2493-403D-BF57-D43773BA387A' 
										ORDER BY C.CreatedAt DESC ";

            return await dbContext.QueryAsync<CommentResponse>(query, new { videoId ,userId});
        }
    }
}
