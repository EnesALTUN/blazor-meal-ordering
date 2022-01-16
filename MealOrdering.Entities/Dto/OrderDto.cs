using MealOrdering.Core.Entities.Abstract;

namespace MealOrdering.Entities.Dto
{
    public class OrderDto : BaseDto, IDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime ExpireDate { get; set; }

        public Guid CreatedUserId { get; set; }

        public Guid SupplierId { get; set; }

        public string CreatedUserFullName { get; set; }

        public string SupplierName { get; set; }
    }
}
