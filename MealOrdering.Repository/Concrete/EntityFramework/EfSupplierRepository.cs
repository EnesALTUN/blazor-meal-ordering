using MealOrdering.Entities.Concrete;
using MealOrdering.Repository.Abstract;
using MealOrdering.Server.Data.EntityFramework.Context;

namespace MealOrdering.Repository.Concrete.EntityFramework
{
    public class EfSupplierRepository : EfGenericRepositoryBase<Supplier>, ISupplierRepository
    {
        public EfSupplierRepository(MealOrderingDbContext context) : base(context)
        {
        }
    }
}
