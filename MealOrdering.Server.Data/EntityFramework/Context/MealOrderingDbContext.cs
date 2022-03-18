using MealOrdering.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace MealOrdering.Server.Data.EntityFramework.Context;

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
        IConfigurationRoot configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();

        optionsBuilder.UseNpgsql(configuration.GetConnectionString("PostgreSqlConn"), mig => mig.MigrationsAssembly("MealOrdering.Server.Data"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public virtual DbSet<User> User { get; set; }

    public virtual DbSet<Order> Order { get; set; }

    public virtual DbSet<SubOrder> SubOrder { get; set; }

    public virtual DbSet<Supplier> Supplier { get; set; }
}