using AutoMapper;
using MealOrdering.Business.Abstract;
using MealOrdering.Entities.Concrete;
using MealOrdering.Entities.Dto;
using MealOrdering.Repository.Abstract;

namespace MealOrdering.Business.Concrete
{
    public class SupplierManager : ISupplierService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SupplierManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SupplierDto> AddSupplier(SupplierDto supplier)
        {
            Supplier dbSupplier = _mapper.Map<Supplier>(supplier);

            await _unitOfWork.Supplier.InsertAsync(dbSupplier);

            await _unitOfWork.SaveAsync();

            return _mapper.Map<SupplierDto>(dbSupplier);
        }

        public async Task<SupplierDto> GetSupplierById(Guid id)
        {
            Supplier dbSupplier = await _unitOfWork.Supplier.GetByIdAsync(id);

            return _mapper.Map<SupplierDto>(dbSupplier);
        }

        public async Task<List<SupplierDto>> GetAllSupplier()
        {
            List<Supplier> dbSupplier = await _unitOfWork.Supplier.GetAllAsync();

            dbSupplier = dbSupplier.OrderBy(o => o.CreatedDate).ToList();

            return _mapper.Map<List<SupplierDto>>(dbSupplier);
        }

        public async Task<SupplierDto> UpdateSupplier(SupplierDto supplier)
        {
            Supplier dbSupplier = await _unitOfWork.Supplier.GetByIdAsync(supplier.Id);

            if (dbSupplier is null)
                throw new Exception("Supplier not found");

            _mapper.Map(supplier, dbSupplier);

            await _unitOfWork.SaveAsync();

            return _mapper.Map<SupplierDto>(dbSupplier);
        }

        public async Task<bool> DeleteSupplierById(Guid id)
        {
            Supplier dbSupplier = await _unitOfWork.Supplier.GetByIdAsync(id);

            if (dbSupplier is null)
                throw new Exception("Supplier not found");

            int orderCount = await _unitOfWork.Order.CountAsync(order => order.SupplierId == id);

            if (orderCount > 0)
                throw new Exception($"There are {orderCount} sub order for the order you are trying to delete");

            await _unitOfWork.Supplier.DeleteAsync(dbSupplier);

            int result = await _unitOfWork.SaveAsync();

            return result > 0;
        }
    }
}
