using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using V_Tube.Application.Abstractions.IServices;
using V_Tube.Application.API_Response;
using V_Tube.Application.DTO;

namespace V_Tube.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideosController (IVideosService service) : ControllerBase
    {
        [HttpPost]
        public async Task<APIResponse<int>> UploadVideo([FromForm] VideoRequest model) =>
            await service.UploadVideo(model);
    }
}
