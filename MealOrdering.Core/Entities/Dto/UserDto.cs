using MealOrdering.Core.Entities.Base;

namespace MealOrdering.Core.Entities.Dto;

public class UserDto : BaseDto, IDto
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string EmailAddress { get; set; }

    public string Password { get; set; }

    public string FullName => $"{FirstName} {LastName}";
}