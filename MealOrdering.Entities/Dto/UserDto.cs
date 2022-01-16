using MealOrdering.Core.Entities.Abstract;

namespace MealOrdering.Entities.Dto
{
    public class UserDto : BaseDto, IDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
