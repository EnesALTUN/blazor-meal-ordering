using MealOrdering.Core.Entities.Base;

namespace MealOrdering.Entities.Request;

public class UserLoginRequestDto : IDto
{
    public string Email { get; set; }

    public string Password { get; set; }
}