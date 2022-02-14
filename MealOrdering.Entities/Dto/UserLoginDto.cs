using MealOrdering.Core.Entities.Base;

namespace MealOrdering.Entities.Dto;

public class UserLoginDto : IDto
{
    public string Email { get; set; }

    public string Password { get; set; }
}