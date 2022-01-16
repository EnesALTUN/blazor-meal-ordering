using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace MealOrdering.Server.Data.EntityFramework.Context
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MealOrderingDbContext>
    {
        public MealOrderingDbContext CreateDbContext(string[] args)
        {
            String connectionString = "Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=f3-EFa1bF";

            var builder = new DbContextOptionsBuilder<MealOrderingDbContext>();

            builder.UseNpgsql(connectionString);

            return new MealOrderingDbContext(builder.Options);
        }
    }
}
