using GL.Company.BLL.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace GL.Company.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationUseCase _authenticationUseCase;

        public AuthenticationController(
            IAuthenticationUseCase authenticationUseCase)
        {

            _authenticationUseCase = authenticationUseCase;
        }

        [HttpPost("token")]
        public IActionResult GenerateToken()
        {
            var token = _authenticationUseCase.GenerateJwtToken();
            return Ok(new { Token = token });
        }
    }
}
