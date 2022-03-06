using MealOrdering.Core.Entities.Dto;
using MealOrdering.Core.Utilities.Results.Abstract;

namespace MealOrdering.Core.Utilities.Security.Jwt;

public interface IJwtHelper
{
    IDataResult<AccessTokenResponseDto> CreateToken(UserDto user);
}