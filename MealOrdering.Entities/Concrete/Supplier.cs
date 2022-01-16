using MealOrdering.Core.Entities.Abstract;

namespace MealOrdering.Entities.Concrete
{
    public class Supplier : BaseEntity, IEntity
    {
        public string Name { get; set; }

        public string WebUrl { get; set; }


        public virtual ICollection<Order> Orders { get; set; }
    }
}
