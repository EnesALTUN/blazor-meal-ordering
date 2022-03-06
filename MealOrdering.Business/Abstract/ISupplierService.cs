using MealOrdering.Core.Utilities.Results.Abstract;
using MealOrdering.Entities.Dto;

namespace MealOrdering.Business.Abstract;

public interface ISupplierService
{
    Task<IDataResult<SupplierDto>> AddSupplier(SupplierDto supplier);

    Task<IDataResult<SupplierDto>> GetSupplierById(Guid id);

    Task<IDataResult<List<SupplierDto>>> GetAllSupplier();

    Task<IDataResult<SupplierDto>> UpdateSupplier(SupplierDto supplier);

    Task<IDataResult<bool>> DeleteSupplierById(Guid id);
}