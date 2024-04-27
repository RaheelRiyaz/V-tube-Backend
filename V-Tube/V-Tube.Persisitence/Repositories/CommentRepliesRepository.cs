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
using V_Tube.Persisitence.Migrations;

namespace V_Tube.Persisitence.Repositories
{
    public class CommentRepliesRepository : BaseRepository<CommentReply>, ICommentRepliesRepository
    {
        public CommentRepliesRepository(VTubeDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<CommentReplyResponse>> FetchRepliesByCommentId(Guid commentId,Guid userId)
        {
            string query = $@"SELECT C.Id,CommentId,Message,RepliedBy,
										UserName as Replier,
										CASE
                                                    WHEN DATEDIFF(MINUTE, C.CreatedAt, GETDATE()) < 60 THEN CAST(DATEDIFF(MINUTE, C.CreatedAt, GETDATE()) AS NVARCHAR(10)) + ' minute(s) ago'
                                                    WHEN DATEDIFF(HOUR, C.CreatedAt, GETDATE()) < 24 THEN CAST(DATEDIFF(HOUR, C.CreatedAt, GETDATE()) AS NVARCHAR(10)) + ' hour(s) ago'
                                                    WHEN DATEDIFF(DAY, C.CreatedAt, GETDATE()) < 365 THEN CAST(DATEDIFF(DAY, C.CreatedAt, GETDATE()) AS NVARCHAR(10)) + ' day(s) ago'
                                                    ELSE CAST(DATEDIFF(YEAR, C.CreatedAt, GETDATE()) AS NVARCHAR(10)) + ' year(s) ago'
                                                END AS TimeAgo,
												CASE WHEN
												EXISTS(SELECT Id FROM Likes WHERE CommentId = C.Id AND  LikedBy = @userId)
												THEN (SELECT IsLiked FROM Likes WHERE CommentId = C.Id AND  LikedBy = @userId)
												ELSE NULL
												END AS HasUserLiked
										FROM CommentReplies C
										INNER JOIN Users U
										ON C.RepliedBy = U.Id WHERE CommentId = @commentId";

            return await dbContext.QueryAsync<CommentReplyResponse>(query, new { commentId, userId });
        }
    }
}
