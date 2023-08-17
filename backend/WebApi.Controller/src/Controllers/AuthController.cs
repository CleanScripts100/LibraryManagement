using Microsoft.AspNetCore.Mvc;
using WebApi.Business.src.Abstractions;
using WebApi.Business.src.Dto;

namespace WebApi.Controller.src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> VerifyCredentials([FromBody] UserCredentialsDto credentials)
        {
            return Ok(await _authService.VerifyCredentials(credentials));
        }
    }
}