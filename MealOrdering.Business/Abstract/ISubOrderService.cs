using MealOrdering.Entities.Dto;

namespace MealOrdering.Business.Abstract
{
    public interface ISubOrderService
    {
        Task<SubOrderDto> AddSubOrder(SubOrderDto subOrder);

        Task<SubOrderDto> GetSubOrderById(Guid id);

        Task<List<SubOrderDto>> GetAllSubOrder();

        Task<SubOrderDto> UpdateSubOrder(SubOrderDto subOrder);

        Task<bool> DeleteSubOrderById(Guid id);
    }
}
