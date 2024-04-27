using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V_Tube.Application.Utilis
{
    public static class AppEncryption
    {
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        public static bool IsTokenExpired(string token)
        {
            var jwtHandler = new JwtSecurityTokenHandler();

            if (jwtHandler.CanReadToken(token))
            {
                var jwtToken = jwtHandler.ReadToken(token) as JwtSecurityToken;
                if (jwtToken != null && jwtToken.Payload.ContainsKey("exp"))
                {
                    var expUnix = Convert.ToDouble(jwtToken.Payload["exp"]);
                    var expirationTime = DateTimeOffset.FromUnixTimeSeconds((long)expUnix).UtcDateTime;

                    return expirationTime <= DateTime.UtcNow;
                }
            }

            return true;
        }
    }

}
