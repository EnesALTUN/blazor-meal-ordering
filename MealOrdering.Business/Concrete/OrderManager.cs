using AutoMapper;
using MealOrdering.Business.Abstract;
using MealOrdering.Core.Utilities.Results.Abstract;
using MealOrdering.Core.Utilities.Results.Concrete;
using MealOrdering.Entities.Concrete;
using MealOrdering.Entities.Dto;
using MealOrdering.Repository.Abstract;
using Microsoft.Extensions.Logging;

namespace MealOrdering.Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly ILogger<OrderManager> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderManager(ILogger<OrderManager> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<OrderDto>> AddOrder(OrderDto order)
        {
            try
            {
                Order dbOrder = _mapper.Map<Order>(order);

                await _unitOfWork.Order.InsertAsync(dbOrder);

                await _unitOfWork.SaveAsync();

                return new SuccessDataResult<OrderDto>(_mapper.Map<OrderDto>(dbOrder));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ErrorDataResult<OrderDto>("An error occurred while adding a order.");
            }
        }

        public async Task<IDataResult<OrderDto>> GetOrderById(Guid id)
        {
            try
            {
                Order dbOrder = await _unitOfWork.Order.GetByIdAsync(id);

                return new SuccessDataResult<OrderDto>(_mapper.Map<OrderDto>(dbOrder));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ErrorDataResult<OrderDto>("An error occurred while pulling a order.");
            }
        }

        public async Task<IDataResult<List<OrderDto>>> GetAllOrder()
        {
            try
            {
                List<Order> dbOrders = await _unitOfWork.Order.GetAllAsync(predicate => true, include => include.Supplier);

                dbOrders = dbOrders.OrderBy(o => o.CreatedDate).ToList();

                return new SuccessDataResult<List<OrderDto>>(_mapper.Map<List<OrderDto>>(dbOrders));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ErrorDataResult<List<OrderDto>>("An error occurred while pulling a orders.");
            }
        }

        public async Task<IDataResult<OrderDto>> UpdateOrder(OrderDto order)
        {
            try
            {
                Order dbOrder = await _unitOfWork.Order.GetByIdAsync(order.Id);

                if (dbOrder is null)
                    throw new Exception("The corresponding record already exists.");

                _mapper.Map(order, dbOrder);

                await _unitOfWork.SaveAsync();

                return new SuccessDataResult<OrderDto>(_mapper.Map<OrderDto>(dbOrder));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ErrorDataResult<OrderDto>("An error occurred during the order update process.");
            }
        }

        public async Task<IDataResult<bool>> DeleteOrderById(Guid id)
        {
            try
            {
                Order dbOrder = await _unitOfWork.Order.GetByIdAsync(id);

                if (dbOrder is null)
                    throw new Exception("Order not found");

                int subOrderCount = await _unitOfWork.SubOrder.CountAsync(x => x.OrderId == id);

                if (subOrderCount > 0)
                    throw new Exception($"There are {subOrderCount} sub order for the order you are trying to delete");

                await _unitOfWork.Order.DeleteAsync(dbOrder);

                int result = await _unitOfWork.SaveAsync();

                return new SuccessDataResult<bool>(result > 0);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ErrorDataResult<bool>("An error occurred while deleting the order.");
            }
        }
    }
}
