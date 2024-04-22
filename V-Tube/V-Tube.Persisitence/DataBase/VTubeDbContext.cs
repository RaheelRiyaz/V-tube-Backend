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
        #endregion Database Tables
    }
}
