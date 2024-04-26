using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V_Tube.Application.Abstractions.IRepository;
using V_Tube.Persisitence.DataBase;
using V_Tube.Persisitence.Repositories;

namespace V_Tube.Persisitence.DI_Container
{
    public static class AssemblyReference
    {
        public static IServiceCollection AddPersisitenceServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContextPool<VTubeDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(nameof(VTubeDbContext)));
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IChannelRepository, ChannelRepository>();
            services.AddScoped<IPlaylistRepository, PlaylistRepository>();
            services.AddScoped<ISubsribeRepository, SubsribeRepository>();
            services.AddScoped<IVideosRepository, VideosRepository>();
            services.AddScoped<INotificationsRepository, NotificationsRepository>();
            services.AddScoped<IVideoViewsRepository, VideoViewsRepository>();
            services.AddScoped<ICommentsRepository, CommentsRepository>();
            services.AddScoped<ILikesRepository, LikesRepository>();
            services.AddScoped<ICommentRepliesRepository, CommentRepliesRepository>();
            return services;
        }
    }
}
