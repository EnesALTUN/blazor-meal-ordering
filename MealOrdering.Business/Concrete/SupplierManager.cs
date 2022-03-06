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
    public class SupplierManager : ISupplierService
    {
        private readonly ILogger<SupplierManager> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SupplierManager(ILogger<SupplierManager> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<SupplierDto>> AddSupplier(SupplierDto supplier)
        {
            try
            {
                Supplier dbSupplier = _mapper.Map<Supplier>(supplier);

                await _unitOfWork.Supplier.InsertAsync(dbSupplier);

                await _unitOfWork.SaveAsync();

                return new SuccessDataResult<SupplierDto>(_mapper.Map<SupplierDto>(dbSupplier));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ErrorDataResult<SupplierDto>("An error occurred while adding a supplier.");
            }
        }

        public async Task<IDataResult<SupplierDto>> GetSupplierById(Guid id)
        {
            try
            {
                Supplier dbSupplier = await _unitOfWork.Supplier.GetByIdAsync(id);

                return new SuccessDataResult<SupplierDto>(_mapper.Map<SupplierDto>(dbSupplier));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ErrorDataResult<SupplierDto>("An error occurred while pulling a supplier.");
            }
        }

        public async Task<IDataResult<List<SupplierDto>>> GetAllSupplier()
        {
            try
            {
                List<Supplier> dbSupplier = await _unitOfWork.Supplier.GetAllAsync();

                dbSupplier = dbSupplier.OrderBy(o => o.CreatedDate).ToList();

                return new SuccessDataResult<List<SupplierDto>>(_mapper.Map<List<SupplierDto>>(dbSupplier));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ErrorDataResult<List<SupplierDto>>("An error occurred while pulling a supplier.");
            }
        }

        public async Task<IDataResult<SupplierDto>> UpdateSupplier(SupplierDto supplier)
        {
            try
            {
                Supplier dbSupplier = await _unitOfWork.Supplier.GetByIdAsync(supplier.Id);

                if (dbSupplier is null)
                    throw new Exception("Supplier not found");

                _mapper.Map(supplier, dbSupplier);

                await _unitOfWork.SaveAsync();

                return new SuccessDataResult<SupplierDto>(_mapper.Map<SupplierDto>(dbSupplier));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ErrorDataResult<SupplierDto>("An error occurred during the supplier update process.");
            }
        }

        public async Task<IDataResult<bool>> DeleteSupplierById(Guid id)
        {
            try
            {
                Supplier dbSupplier = await _unitOfWork.Supplier.GetByIdAsync(id);

                if (dbSupplier is null)
                    throw new Exception("Supplier not found");

                int orderCount = await _unitOfWork.Order.CountAsync(order => order.SupplierId == id);

                if (orderCount > 0)
                    throw new Exception($"There are {orderCount} sub order for the order you are trying to delete");

                await _unitOfWork.Supplier.DeleteAsync(dbSupplier);

                int result = await _unitOfWork.SaveAsync();

                return new SuccessDataResult<bool>(result > 0);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ErrorDataResult<bool>("An error occurred while deleting the supplier.");
            }
        }
    }
}