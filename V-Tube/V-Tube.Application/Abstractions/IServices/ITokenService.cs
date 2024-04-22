using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V_Tube.Domain.Models;

namespace V_Tube.Application.Abstractions.IServices
{
    public interface ITokenService
    {
        string GenerateAccessToken(User model);
        string GenerateRefreshToken(Guid id);
    }
}
