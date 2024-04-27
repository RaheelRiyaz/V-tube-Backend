using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
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
    public class VideosRepository : BaseRepository<Video>, IVideosRepository
    {

        #region BaseQuery
        private const string baseQuery = $@"  SELECT 
                                            VideoId,
                                            ChannelId,
                                            PlayListId,
                                            VideoTitle,
                                            VideoDescription,
                                            Thumbnail,
                                            Duration,
                                            ChannelName,
                                            VideoUrl,
                                            ProfileUrl,
	                                        CreatedAt,
                                            TotalViews,
											TotalComments,
											TotalDislikes,
											Totallikes,
											HasUserLiked,
                                            CASE
                                                WHEN TotalViews < 1000 THEN CAST(TotalViews AS NVARCHAR(10)) + ' views'
                                                WHEN TotalViews >= 1000 AND TotalViews < 100000 THEN CAST(TotalViews / 1000 AS NVARCHAR(10)) + 'k views'
                                                WHEN TotalViews >= 100000 AND TotalViews < 1000000 THEN CAST(TotalViews / 100000 AS NVARCHAR(10)) + ' lakh views'
                                                WHEN TotalViews >= 1000000 AND TotalViews < 10000000 THEN CAST(TotalViews / 1000000 AS NVARCHAR(10)) + ' million views'
                                                WHEN TotalViews >= 10000000 AND TotalViews < 100000000 THEN CAST(TotalViews / 10000000 AS NVARCHAR(10)) + ' crore views'
                                                ELSE CAST(TotalViews / 1000000000 AS NVARCHAR(10)) + ' billion views'
                                            END AS TotalViewsFormatted,
                                            TimeAgo
                                        FROM (
                                            SELECT 
                                                V.Id AS VideoId,
                                                V.ChannelId,
                                                PlayListId,
                                                V.Title AS VideoTitle,
                                                V.[Description] AS VideoDescription,
                                                Thumbnail,
		                                        V.CreatedAt,
                                                Duration,
                                                C.[Name] AS ChannelName,
                                                V.[Url] AS VideoUrl,
                                                ProfileUrl,
												CASE WHEN
												EXISTS(SELECT Id FROM Likes WHERE VideoId = V.Id AND  LikedBy = '861B6371-426C-4868-ACD7-F96DBE227456')
												THEN (SELECT IsLiked FROM Likes WHERE VideoId = V.Id AND  LikedBy = '861B6371-426C-4868-ACD7-F96DBE227456')
												ELSE NULL
												END AS HasUserLiked,
												(
												(SELECT COUNT(Id) FROM Comments WHERE EntityId = V.Id)
												+
												(SELECT COUNT(ID) FROM CommentReplies
												WHERE CommentId IN (SELECT Id FROM Comments WHERE EntityId = V.Id))) AS TotalComments,
                                                (SELECT COUNT(Id) FROM VideoViews WHERE VideoId = V.Id) AS TotalViews,
											    (SELECT COUNT(Id) FROM Likes WHERE VideoId = V.Id AND IsLiked = 1 AND CommentId IS NULL AND VideoId IS NOT NULL) AS TotalLikes,
											    (SELECT COUNT(Id) FROM Likes WHERE VideoId = V.Id AND IsLiked = 0 AND CommentId IS NULL AND VideoId IS NOT NULL) AS TotalDislikes,

                                                CASE
                                                    WHEN DATEDIFF(MINUTE, V.CreatedAt, GETDATE()) < 60 THEN CAST(DATEDIFF(MINUTE, V.CreatedAt, GETDATE()) AS NVARCHAR(10)) + ' minute(s) ago'
                                                    WHEN DATEDIFF(HOUR, V.CreatedAt, GETDATE()) < 24 THEN CAST(DATEDIFF(HOUR, V.CreatedAt, GETDATE()) AS NVARCHAR(10)) + ' hour(s) ago'
                                                    WHEN DATEDIFF(DAY, V.CreatedAt, GETDATE()) < 365 THEN CAST(DATEDIFF(DAY, V.CreatedAt, GETDATE()) AS NVARCHAR(10)) + ' day(s) ago'
                                                    ELSE CAST(DATEDIFF(YEAR, V.CreatedAt, GETDATE()) AS NVARCHAR(10)) + ' year(s) ago'
                                                END AS TimeAgo
                                            FROM 
                                                Videos V
                                            INNER JOIN 
                                                Channels C ON C.Id = V.ChannelId
                                        ) AS SubQuery";
        #endregion BaseQuery

        public VideosRepository(VTubeDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<VideoDisplayResponse>> GetDisplayVideos(VideoFilter model,Guid userId, bool hasSubscribedAnyChannel)
        {
            var paginationQuery = $@" ORDER BY CreatedAt DESC OFFSET ((@pageNo - 1) * @pageSize) ROWS FETCH NEXT @pageSize ROWS ONLY";

            StringBuilder query = new StringBuilder($@" {baseQuery} ");


            if(model.PlayListId is null && model.ChannelId is not null)
            {
                query.Append($@" WHERE ChannelId = @channelId {paginationQuery} ");

                return await dbContext.QueryAsync<VideoDisplayResponse>(query.ToString(), new { model.PageNo, model.PageSize,model.ChannelId,userId});
            }

            if (model.PlayListId is not null && model.ChannelId is null)
            {
                query.Append($@" WHERE PlayListId = @playListId {paginationQuery}");

                return await dbContext.QueryAsync<VideoDisplayResponse>(query.ToString(), new { model.PageNo, model.PageSize, model.PlayListId, userId });
            }

            if (hasSubscribedAnyChannel)
                query.Append($@" WHERE ChannelId IN (SELECT ChannelId FROM Subscribers 
                    WHERE UserId = @userId) {paginationQuery} ");

            else query.Append($" {paginationQuery} ");

            return await dbContext.QueryAsync<VideoDisplayResponse>(query.ToString(), new { model.PageNo, model.PageSize, userId });
        }

        public async Task<byte> UploadVideoAndNotifySubscribers(SPVideoUploadRequest model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ChannelId", model.ChannelId);
            parameters.Add("@PlayListId", model.PlaylistId);
            parameters.Add("@Title", model.Title);
            parameters.Add("@Description", model.Description);
            parameters.Add("@Thumbnail", model.Thumbnail);
            parameters.Add("@Duration", model.Duration);
            parameters.Add("@Url", model.Url);
            parameters.Add("@Response", dbType: DbType.Byte, direction: ParameterDirection.Output);

            await dbContext.ExecuteAsync("Sp_UploadVideo", parameters, commandType: CommandType.StoredProcedure);

            var res = parameters.Get<byte>("@Response");
            return res;
        }
    }
}
