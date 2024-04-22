using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V_Tube.Application.Abstractions.IServices;

namespace V_Tube.Infrastructure.Services
{
    public class ContextService : IContextService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public ContextService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }


        public Guid GetContextId()
        {
            var claim = httpContextAccessor?.HttpContext?.User.Claims.FirstOrDefault(_ => _.Type == "Id");

            return claim is not null ? Guid.Parse(claim.Value) : Guid.Empty;
        }
    }
}
