namespace MealOrdering.Core.Entities.Base
{
    public class BaseDto
    {
        public virtual Guid Id { get; set; } = Guid.NewGuid();

        public virtual bool IsActive { get; set; } = true;

        public virtual bool IsDeleted { get; set; } = false;

        public virtual DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public virtual DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
    }
}
