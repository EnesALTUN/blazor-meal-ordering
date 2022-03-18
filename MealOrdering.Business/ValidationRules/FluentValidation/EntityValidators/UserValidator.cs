using FluentValidation;
using MealOrdering.Entities.Concrete;

namespace MealOrdering.Business.ValidationRules.FluentValidation.EntityValidators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.Id)
                .NotNull()
                .WithName("id");

            RuleFor(user => user.FirstName)
                .NotNull()
                .Length(3, 100);

            RuleFor(user => user.LastName)
                .NotNull()
                .Length(3, 100);

            RuleFor(user => user.EmailAddress)
                .NotNull()
                .EmailAddress();

            RuleFor(user => user.Password)
                .NotNull();

            RuleFor(user => user.IsActive)
                .NotNull();

            RuleFor(user => user.IsDeleted)
                .NotNull();

            RuleFor(user => user.CreatedDate)
                .NotNull();

            RuleFor(user => user.ModifiedDate)
                .NotNull();
        }
    }
}
