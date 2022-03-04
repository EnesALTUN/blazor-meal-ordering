using MealOrdering.Business.Abstract;
using MealOrdering.Core.Entities.Dto;
using MealOrdering.Core.Utilities;
using MealOrdering.Core.Utilities.Security.Jwt;
using MealOrdering.Entities.Dto;

namespace MealOrdering.Business.Concrete;

public class AuthManager : IAuthService
{
    private readonly IUserService _userService;
    private readonly IJwtHelper _jwtHelper;

    public AuthManager(IUserService userService, IJwtHelper jwtHelper)
    {
        _userService = userService;
        _jwtHelper = jwtHelper;
    }

    public async Task<AccessTokenResponseDto> Login(UserLoginDto user)
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

            throw;
        }
    }
}