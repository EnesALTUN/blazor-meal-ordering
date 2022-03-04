using MealOrdering.Entities.Concrete;
using MealOrdering.Server.Data.EntityFramework.Builder;
using Microsoft.EntityFrameworkCore;

namespace MealOrdering.Server.Data.EntityFramework.Context
{
    public class MealOrderingDbContext : DbContext
    {
        public MealOrderingDbContext()
        {

        }

        public MealOrderingDbContext(DbContextOptions<MealOrderingDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=<password>", mig => mig.MigrationsAssembly("MealOrdering.Server.Data"));
        }

        public virtual DbSet<User> User { get; set; }

        public virtual DbSet<Order> Order { get; set; }

        public virtual DbSet<SubOrder> SubOrder { get; set; }

        public virtual DbSet<Supplier> Supplier { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(DbModelBuilder.DbBuilder(modelBuilder));
        }
    }

}
