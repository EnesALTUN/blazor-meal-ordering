using MealOrdering.Core.Entities.Dto;
using MealOrdering.Entities.Dto;

namespace MealOrdering.Business.Abstract;

public interface IAuthService
{
    Task<AccessTokenResponseDto> Login(UserLoginDto user);
}