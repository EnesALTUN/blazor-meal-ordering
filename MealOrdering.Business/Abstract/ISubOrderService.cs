using MealOrdering.Core.Utilities.Results.Abstract;
using MealOrdering.Entities.Dto;

namespace MealOrdering.Business.Abstract;

public interface ISubOrderService
{
    Task<IDataResult<SubOrderDto>> AddSubOrder(SubOrderDto subOrder);

    Task<IDataResult<SubOrderDto>> GetSubOrderById(Guid id);

    Task<IDataResult<List<SubOrderDto>>> GetAllSubOrder();

    Task<IDataResult<SubOrderDto>> UpdateSubOrder(SubOrderDto subOrder);

    Task<IDataResult<bool>> DeleteSubOrderById(Guid id);
}