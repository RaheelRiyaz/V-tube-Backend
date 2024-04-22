using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using V_Tube.Application.Abstractions.IServices;
using V_Tube.Application.API_Response;
using V_Tube.Application.DTO;

namespace V_Tube.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserService service) : ControllerBase
    {
        [HttpPost("signup")]
        public async Task<APIResponse<int>> Signup(UserRequest model) =>
            await service.Signup(model);


        [HttpPost("login")]
        public async Task<APIResponse<LoginResponse>> Login(UserRequest model) =>
            await service.Login(model);


        [HttpGet("logout")]
        public async Task<APIResponse<int>> Logout() =>
            await service.Logout();
    }
}
