using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V_Tube.Domain.Models;

namespace V_Tube.Persisitence.DataBase
{
    public class VTubeDbContext : DbContext
    {
        public VTubeDbContext(DbContextOptions options) : base(options)
        {
        }





        #region Database Tables
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Channel> Channels { get; set; } = null!;
        public DbSet<PlayList> PlayLists { get; set; } = null!;
        public DbSet<Video> Videos { get; set; } = null!;
        public DbSet<Subscriber> Subscribers { get; set; } = null!;
        public DbSet<Notification> Notifications { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;
        public DbSet<Likes> Likes { get; set; } = null!;
        public DbSet<CommentReply> CommentReplies { get; set; } = null!;
        public DbSet<VideoViews> VideoViews { get; set; } = null!;
        #endregion Database Tables
    }
}
