using MealOrdering.Entities.Dto;

namespace MealOrdering.Business.Abstract
{
    public interface ISupplierService
    {
        Task<SupplierDto> AddSupplier(SupplierDto supplier);

        Task<SupplierDto> GetSupplierById(Guid id);

        Task<List<SupplierDto>> GetAllSupplier();

        Task<SupplierDto> UpdateSupplier(SupplierDto supplier);

        Task<bool> DeleteSupplierById(Guid id);
    }
}
