using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V_Tube.Domain.Models
{
    public class Channel : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Handle { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ProfileUrl { get; set; } = null!;



        #region Navigational Properties
        [ForeignKey(nameof(Id))]
        public User User { get; set; } = null!;
        #endregion Navigational Properties

    }
}
