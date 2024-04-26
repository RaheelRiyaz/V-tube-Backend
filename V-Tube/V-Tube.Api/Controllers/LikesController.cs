using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using V_Tube.Application.Abstractions.IServices;
using V_Tube.Application.API_Response;
using V_Tube.Application.DTO;

namespace V_Tube.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController (ILikeService service) : ControllerBase
    {
        [HttpPost]
        public async Task<APIResponse<int>> AddLike(LikeRequest model) =>
            await service.AddLike(model);
    }
}
