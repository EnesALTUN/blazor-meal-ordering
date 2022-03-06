using MealOrdering.Business.Abstract;
using MealOrdering.Core.Entities.Dto;
using MealOrdering.Core.Utilities.Results.Concrete;
using MealOrdering.Entities.Request;
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
        public async Task<ApiResult<AccessTokenResponseDto>> Login([FromBody] UserLoginRequestDto user)
        {
            var loginResult = await _authService.Login(user);

            return new ApiResult<AccessTokenResponseDto>
            {
                Success = loginResult.Success,
                Message = loginResult.Message,
                Data = loginResult.Data
            };
        }
    }
}