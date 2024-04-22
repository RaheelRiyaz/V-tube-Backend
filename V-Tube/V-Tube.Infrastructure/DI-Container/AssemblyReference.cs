using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V_Tube.Application.Abstractions.IServices;
using V_Tube.Infrastructure.Services;

namespace V_Tube.Infrastructure.DI_Container
{
    public static class AssemblyReference
    {
        public static IServiceCollection AddInfraStructureServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IContextService,ContextService>();
            return services;
        }
    }
}
