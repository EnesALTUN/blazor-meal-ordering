using MealOrdering.Business.Abstract;
using MealOrdering.Core.Entities.Dto;
using MealOrdering.Entities.Request;
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

        [HttpPost("Login")]
        public async Task<ServiceResponse<AccessTokenResponseDto>> Login([FromBody] UserLoginRequestDto user)
        {
            return new ServiceResponse<AccessTokenResponseDto>
            {
                Data = await _authService.Login(user)
            };
        }
    }
}