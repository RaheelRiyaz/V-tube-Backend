using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V_Tube.Domain.Models
{
    public class User : BaseEntity
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public DateTime RefreshExpiry { get; set; }
    }
}
