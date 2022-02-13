using AutoMapper;
using MealOrdering.Business.Abstract;
using MealOrdering.Entities.Concrete;
using MealOrdering.Entities.Dto;
using MealOrdering.Repository.Abstract;

namespace MealOrdering.Business.Concrete
{
    public class SubOrderManager : ISubOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SubOrderManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SubOrderDto> AddSubOrder(SubOrderDto subOrder)
        {
            SubOrder dbSubOrder = await _unitOfWork.SubOrder.GetByIdAsync(subOrder.Id);

            if (dbSubOrder is not null)
                throw new Exception("The corresponding record already exists.");

            dbSubOrder = _mapper.Map<SubOrder>(subOrder);

            await _unitOfWork.SubOrder.InsertAsync(dbSubOrder);

            await _unitOfWork.SaveAsync();

            return _mapper.Map<SubOrderDto>(dbSubOrder);
        }

        public async Task<SubOrderDto> GetSubOrderById(Guid id)
        {
            SubOrder dbSubOrder = await _unitOfWork.SubOrder.GetByIdAsync(id);

            return _mapper.Map<SubOrderDto>(dbSubOrder);
        }

        public async Task<List<SubOrderDto>> GetAllSubOrder()
        {
            List<SubOrder> dbSubOrders = await _unitOfWork.SubOrder.GetAllAsync(predicate => true, include => include.Order);

            return _mapper.Map<List<SubOrderDto>>(dbSubOrders);
        }

        public async Task<SubOrderDto> UpdateSubOrder(SubOrderDto subOrder)
        {
            SubOrder dbSubOrder = await _unitOfWork.SubOrder.GetByIdAsync(subOrder.Id);

            if (dbSubOrder is null)
                throw new Exception("The corresponding record already exists.");

            _mapper.Map(subOrder, dbSubOrder);

            await _unitOfWork.SaveAsync();

            return _mapper.Map<SubOrderDto>(dbSubOrder);
        }

        public async Task<bool> DeleteSubOrderById(Guid id)
        {
            SubOrder dbSubOrder = await _unitOfWork.SubOrder.GetByIdAsync(id);

            if (dbSubOrder is null)
                throw new Exception("Order not found");

            await _unitOfWork.SubOrder.DeleteAsync(dbSubOrder);

            int result = await _unitOfWork.SaveAsync();

            return result > 0;
        }
    }
}
