using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V_Tube.Domain.Models
{
    public class WatchHistory : WatchLater
    {
        public decimal DurationViewed { get; set; }
    }
}
