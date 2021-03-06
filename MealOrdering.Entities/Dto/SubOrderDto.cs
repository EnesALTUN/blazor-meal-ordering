using MealOrdering.Core.Entities.Base;

namespace MealOrdering.Entities.Dto
{
    public class SubOrderDto : BaseDto, IDto
    {
        public Guid CreatedUserId { get; set; }

        public Guid OrderId { get; set; }

        public string Description { get; set; }

        public string CreatedUserFullName { get; set; }

        public string OrderName { get; set; }
    }
}
