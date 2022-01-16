using MealOrdering.Entities.Concrete;
using MealOrdering.Server.Data.EntityFramework.Builder;
using Microsoft.EntityFrameworkCore;

namespace MealOrdering.Server.Data.EntityFramework.Context
{
    public class MealOrderingDbContext : DbContext
    {
        public MealOrderingDbContext(DbContextOptions<MealOrderingDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(DbModelBuilder.DbBuilder(modelBuilder));
        }
    }

}
