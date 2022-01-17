using MealOrdering.Entities.Concrete;
using MealOrdering.Repository.Abstract;
using MealOrdering.Server.Data.EntityFramework.Context;

namespace MealOrdering.Repository.Concrete.EntityFramework
{
    public class EfSubOrderRepository : EfGenericRepositoryBase<SubOrder>, ISubOrderRepository
    {
        public EfSubOrderRepository(MealOrderingDbContext context) : base(context)
        {
        }
    }
}
