using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V_Tube.Application.Abstractions.IRepository;
using V_Tube.Domain.Models;
using V_Tube.Persisitence.DataBase;
using V_Tube.Persisitence.Migrations;

namespace V_Tube.Persisitence.Repositories
{
    public class CommentRepliesRepository : BaseRepository<CommentReply>, ICommentRepliesRepository
    {
        public CommentRepliesRepository(VTubeDbContext dbContext) : base(dbContext)
        {
        }
    }
}
