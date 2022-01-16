using MealOrdering.Core.Entities.Abstract;

namespace MealOrdering.Entities.Dto
{
    public class SupplierDto : BaseDto, IDto
    {
        public string Name { get; set; }

        public string WebUrl { get; set; }
    }
}
