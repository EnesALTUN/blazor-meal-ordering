using MealOrdering.Entities.Concrete;
using MealOrdering.Repository.Abstract;
using MealOrdering.Server.Data.EntityFramework.Context;

namespace MealOrdering.Repository.Concrete.EntityFramework
{
    public class EfUserRepository : EfGenericRepositoryBase<User>, IUserRepository
    {
        public EfUserRepository(MealOrderingDbContext context) : base(context)
        {
        }
    }
}
