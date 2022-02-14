using MealOrdering.Core.Entities.Base;

namespace MealOrdering.Entities.Concrete
{
    public class SubOrder : BaseEntity, IEntity
    {
        public Guid CreatedUserId { get; set; }

        public Guid OrderId { get; set; }

        public string Description { get; set; }


        public virtual User CreatedUser { get; set; }

        public virtual Order Order { get; set; }
    }
}
