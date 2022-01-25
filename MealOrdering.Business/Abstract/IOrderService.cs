using MealOrdering.Entities.Dto;

namespace MealOrdering.Business.Abstract
{
    public interface IOrderService
    {
        Task<OrderDto> AddOrder(OrderDto order);

        Task<OrderDto> GetOrderById(Guid id);

        Task<List<OrderDto>> GetAllOrder();

        Task<OrderDto> UpdateOrder(OrderDto order);

        Task<bool> DeleteOrderById(Guid id);
    }
}
