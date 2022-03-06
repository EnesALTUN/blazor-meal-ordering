using MealOrdering.Business.Abstract;
using MealOrdering.Core.Entities.Dto;
using MealOrdering.Core.Utilities;
using MealOrdering.Core.Utilities.Results.Abstract;
using MealOrdering.Core.Utilities.Results.Concrete;
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

    public async Task<IDataResult<AccessTokenResponseDto>> Login(UserLoginRequestDto user)
    {
        try
        {
            IDataResult<UserDto> userDto = await _userService.GetUserByEmail(user.Email);

            if (userDto.Success)
            {
                if (!HashingHelper.VerifyHashedPassword(userDto.Data.Password, user.Password))
                    throw new Exception("Incorrect password");

                IDataResult<AccessTokenResponseDto> createdToken = _jwtHelper.CreateToken(userDto.Data);

                if (!createdToken.Success)
                    throw new Exception(createdToken.Message);

                return new SuccessDataResult<AccessTokenResponseDto>(createdToken.Data, "User login successful.");
            }
            else
                return new ErrorDataResult<AccessTokenResponseDto>("An error occurred during the user login process.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new ErrorDataResult<AccessTokenResponseDto>("An error occurred during the user login process.");
        }
    }
}