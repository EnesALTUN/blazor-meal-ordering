using MealOrdering.Business.Abstract;
using MealOrdering.Entities.Dto;

namespace MealOrdering.Business.Concrete;

public class AuthManager : IAuthService
{
    public string Login(UserLoginDto user)
    {
        return "TestToken";
    }
}