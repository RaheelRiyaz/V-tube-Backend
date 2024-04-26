using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V_Tube.Application.DTO
{
    public record UserRequest
    (
    string UserName,
    string Email,
    string Password
    );

   
    public record LoginResponse
        (
        string AccessToken,
        string RefreshToken
        );
}
