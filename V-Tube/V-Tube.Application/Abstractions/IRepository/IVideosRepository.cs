
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V_Tube.Application.Abstractions.IServices;
using V_Tube.Domain.Models;

namespace V_Tube.Application.Abstractions.IRepository
{
    public interface IVideosRepository : IBaseRepository<Video>
    {
    }
}
