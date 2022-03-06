using MealOrdering.Core.Utilities.Results.Abstract;
using MealOrdering.Entities.Dto;

namespace MealOrdering.Business.Abstract;

public interface IOrderService
{
    Task<IDataResult<OrderDto>> AddOrder(OrderDto order);

    Task<IDataResult<OrderDto>> GetOrderById(Guid id);

    Task<IDataResult<List<OrderDto>>> GetAllOrder();

    Task<IDataResult<OrderDto>> UpdateOrder(OrderDto order);

    Task<IDataResult<bool>> DeleteOrderById(Guid id);
}