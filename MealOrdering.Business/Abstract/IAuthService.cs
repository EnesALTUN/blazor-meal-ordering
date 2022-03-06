using MealOrdering.Core.Entities.Dto;
using MealOrdering.Core.Utilities.Results.Abstract;
using MealOrdering.Entities.Request;

namespace MealOrdering.Business.Abstract;

public interface IAuthService
{
    Task<IDataResult<AccessTokenResponseDto>> Login(UserLoginRequestDto user);
}