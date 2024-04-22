using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V_Tube.Persisitence.DataBase
{
    public class VTubeDbContext : DbContext
    {
        public VTubeDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
