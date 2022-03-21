namespace MealOrdering.Core.Entities.Base
{
    public class BaseDto
    {
        public virtual Guid Id { get; set; }

        public virtual bool IsActive { get; set; }

        public virtual bool IsDeleted { get; set; }

        public virtual DateTime CreatedDate { get; set; }

        public virtual DateTime ModifiedDate { get; set; }
    }
}
