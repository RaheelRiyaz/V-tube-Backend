using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V_Tube.Persisitence.DataBase;

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

            return services;
        }
    }
}
