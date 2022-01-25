using AutoMapper;
using MealOrdering.Business.Abstract;
using MealOrdering.Entities.Concrete;
using MealOrdering.Entities.Dto;
using MealOrdering.Repository.Abstract;

namespace MealOrdering.Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderDto> AddOrder(OrderDto order)
        {
            Order dbOrder = await _unitOfWork.Order.GetByIdAsync(order.Id);

            if (dbOrder is not null)
                throw new Exception("The corresponding record already exists.");

            dbOrder = _mapper.Map<Order>(order);

            await _unitOfWork.Order.InsertAsync(dbOrder);

            await _unitOfWork.SaveAsync();

            return _mapper.Map<OrderDto>(dbOrder);
        }

        public async Task<OrderDto> GetOrderById(Guid id)
        {
            Order dbOrder = await _unitOfWork.Order.GetByIdAsync(id);

            return _mapper.Map<OrderDto>(dbOrder);
        }

        public async Task<List<OrderDto>> GetAllOrder()
        {
            List<Order> dbOrders = await _unitOfWork.Order.GetAllAsync();

            return _mapper.Map<List<OrderDto>>(dbOrders);
        }

        public async Task<OrderDto> UpdateOrder(OrderDto order)
        {
            Order dbOrder = await _unitOfWork.Order.GetByIdAsync(order.Id);

            if (dbOrder is null)
                throw new Exception("The corresponding record already exists.");

            _mapper.Map(order, dbOrder);

            await _unitOfWork.SaveAsync();

            return _mapper.Map<OrderDto>(dbOrder);
        }

        public async Task<bool> DeleteOrderById(Guid id)
        {
            Order dbOrder = await _unitOfWork.Order.GetByIdAsync(id);

            if (dbOrder is null)
                throw new Exception("Order not found");

            await _unitOfWork.Order.DeleteAsync(dbOrder);

            int result = await _unitOfWork.SaveAsync();

            return result > 0;
        }
    }
}
