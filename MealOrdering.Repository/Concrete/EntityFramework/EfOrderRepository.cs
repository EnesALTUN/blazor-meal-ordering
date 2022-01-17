using MealOrdering.Entities.Concrete;
using MealOrdering.Repository.Abstract;
using MealOrdering.Server.Data.EntityFramework.Context;

namespace MealOrdering.Repository.Concrete.EntityFramework
{
    public class EfOrderRepository : EfGenericRepositoryBase<Order>, IOrderRepository
    {
        public EfOrderRepository(MealOrderingDbContext context) : base(context)
        {
        }
    }
}
