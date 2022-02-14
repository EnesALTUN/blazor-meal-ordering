using MealOrdering.Core.Entities.Base;

namespace MealOrdering.Entities.Concrete
{
    public class Order : BaseEntity, IEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime ExpireDate { get; set; }

        public Guid CreatedUserId { get; set; }

        public Guid SupplierId { get; set; }


        public virtual User CreateUser { get; set; }

        public virtual Supplier Supplier { get; set; }

        public virtual ICollection<SubOrder> SubOrders { get; set; }
    }
}
