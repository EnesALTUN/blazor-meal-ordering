using MealOrdering.Core.Entities.Base;

namespace MealOrdering.Entities.Concrete
{
    public class User : BaseEntity, IEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }


        public virtual ICollection<Order> Orders { get; set;}

        public virtual ICollection<SubOrder> SubOrders { get; set;}

    }
}
