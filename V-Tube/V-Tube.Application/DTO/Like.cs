using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V_Tube.Domain.Enums;

namespace V_Tube.Application.DTO
{
    public record LikeRequest
        (
        LikeType LikeType,
        Guid? VideoId,
        Guid? CommentId,
        bool ? IsLiked
        );
}
