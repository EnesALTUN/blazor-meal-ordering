using FluentValidation;
using MealOrdering.Entities.Concrete;

namespace MealOrdering.Business.ValidationRules.FluentValidation.EntityValidators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.Id).NotEmpty();
            RuleFor(user => user.FirstName).NotEmpty();
            RuleFor(user => user.FirstName).Length(3, 100);
            RuleFor(user => user.LastName).NotEmpty();
            RuleFor(user => user.LastName).Length(3, 100);
            RuleFor(user => user.EmailAddress).NotEmpty();
            RuleFor(user => user.EmailAddress).EmailAddress();
            RuleFor(user => user.IsActive).NotEmpty();
            RuleFor(user => user.IsDeleted).NotEmpty();
            RuleFor(user => user.CreatedDate).NotEmpty();
            RuleFor(user => user.ModifiedDate).NotEmpty();
        }
    }
}
