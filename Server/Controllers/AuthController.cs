using MealOrdering.Business.Abstract;
using MealOrdering.Entities.Dto;
using MealOrdering.Entities.Response;
using Microsoft.AspNetCore.Mvc;

namespace MealOrdering.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet("Login")]
        public async Task<ServiceResponse<string>> Login([FromBody] UserLoginDto user)
        {
            return new ServiceResponse<string>
            {
                Data = ""
            };
        }
    }
}
