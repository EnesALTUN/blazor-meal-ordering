using MealOrdering.Business.Abstract;
using MealOrdering.Core.Entities.Dto;
using MealOrdering.Core.Utilities;
using MealOrdering.Core.Utilities.Security.Jwt;
using MealOrdering.Entities.Request;
using Microsoft.Extensions.Logging;

namespace MealOrdering.Business.Concrete;

public class AuthManager : IAuthService
{
    private readonly ILogger<AuthManager> _logger;
    private readonly IUserService _userService;
    private readonly IJwtHelper _jwtHelper;

    public AuthManager(ILogger<AuthManager> logger, IUserService userService, IJwtHelper jwtHelper)
    {
        _logger = logger;
        _userService = userService;
        _jwtHelper = jwtHelper;
    }

    public async Task<AccessTokenResponseDto> Login(UserLoginRequestDto user)
    {
        try
        {
            UserDto userDto = await _userService.GetUserByEmail(user.Email);

            if (!HashingHelper.VerifyHashedPassword(userDto.Password, user.Password))
                throw new Exception("Username or password incorrect");

            return _jwtHelper.CreateToken(userDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new AccessTokenResponseDto { };
        }
    }
}