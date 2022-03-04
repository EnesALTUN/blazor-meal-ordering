using MealOrdering.Core.Entities.Dto;

namespace MealOrdering.Core.Utilities.Security.Jwt;

public interface IJwtHelper
{
    AccessTokenResponseDto CreateToken(UserDto user);
}