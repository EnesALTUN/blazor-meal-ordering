using MealOrdering.Entities.Dto;

namespace MealOrdering.Business.Abstract;

public interface IAuthService
{
    string Login(UserLoginDto user);
}