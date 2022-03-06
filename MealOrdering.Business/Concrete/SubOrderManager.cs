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
    public class SubOrderManager : ISubOrderService
    {
        private readonly ILogger<SubOrderManager> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SubOrderManager(ILogger<SubOrderManager> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<SubOrderDto>> AddSubOrder(SubOrderDto subOrder)
        {
            try
            {
                Order order = await _unitOfWork.Order.GetByIdAsync(subOrder.OrderId);

                if (order is null)
                    throw new Exception("The main order not found");

                if (order.ExpireDate <= DateTime.UtcNow)
                    throw new Exception("You cannot create sub order. It is expired !!");

                SubOrder dbSubOrder = _mapper.Map<SubOrder>(subOrder);

                await _unitOfWork.SubOrder.InsertAsync(dbSubOrder);

                await _unitOfWork.SaveAsync();

                return new SuccessDataResult<SubOrderDto>(_mapper.Map<SubOrderDto>(dbSubOrder));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ErrorDataResult<SubOrderDto>("An error occurred while adding a sub order.");
            }
        }

        public async Task<IDataResult<SubOrderDto>> GetSubOrderById(Guid id)
        {
            try
            {
                SubOrder dbSubOrder = await _unitOfWork.SubOrder.GetAsync(filter => filter.Id == id, include => include.Order);

                return new SuccessDataResult<SubOrderDto>(_mapper.Map<SubOrderDto>(dbSubOrder));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ErrorDataResult<SubOrderDto>("An error occurred while adding a sub order.");
            }
        }

        public async Task<IDataResult<List<SubOrderDto>>> GetAllSubOrder()
        {
            try
            {
                List<SubOrder> dbSubOrders = await _unitOfWork.SubOrder.GetAllAsync(predicate => true, include => include.Order);

                dbSubOrders = dbSubOrders.OrderBy(o => o.CreatedDate).ToList();

                return new SuccessDataResult<List<SubOrderDto>>(_mapper.Map<List<SubOrderDto>>(dbSubOrders));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ErrorDataResult<List<SubOrderDto>>("An error occurred while pulling a sub orders.");
            }
        }

        public async Task<IDataResult<SubOrderDto>> UpdateSubOrder(SubOrderDto subOrder)
        {
            try
            {
                SubOrder dbSubOrder = await _unitOfWork.SubOrder.GetByIdAsync(subOrder.Id);

                if (dbSubOrder is null)
                    throw new Exception("The corresponding record already exists.");

                _mapper.Map(subOrder, dbSubOrder);

                await _unitOfWork.SaveAsync();

                return new SuccessDataResult<SubOrderDto>(_mapper.Map<SubOrderDto>(dbSubOrder));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ErrorDataResult<SubOrderDto>("An error occurred during the sub order update process.");
            }
        }

        public async Task<IDataResult<bool>> DeleteSubOrderById(Guid id)
        {
            try
            {
                SubOrder dbSubOrder = await _unitOfWork.SubOrder.GetByIdAsync(id);

                if (dbSubOrder is null)
                    throw new Exception("Sub order not found");

                await _unitOfWork.SubOrder.DeleteAsync(dbSubOrder);

                int result = await _unitOfWork.SaveAsync();

                return new SuccessDataResult<bool>(result > 0);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ErrorDataResult<bool>("An error occurred while deleting the sub order.");
            }
        }
    }
}