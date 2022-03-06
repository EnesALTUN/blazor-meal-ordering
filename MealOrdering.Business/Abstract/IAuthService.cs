using MealOrdering.Core.Entities.Dto;
using MealOrdering.Entities.Request;

namespace MealOrdering.Business.Abstract;

public interface IAuthService
{
    Task<AccessTokenResponseDto> Login(UserLoginRequestDto user);
}