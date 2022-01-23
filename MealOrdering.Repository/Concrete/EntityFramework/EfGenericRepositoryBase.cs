using MealOrdering.Core.Entities.Abstract;
using MealOrdering.Repository.Abstract;
using MealOrdering.Server.Data.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MealOrdering.Repository.Concrete.EntityFramework
{
    public class EfGenericRepositoryBase<TEntity> : IGenericRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        private readonly MealOrderingDbContext _context;
        private readonly DbSet<TEntity> _table;

        public EfGenericRepositoryBase(MealOrderingDbContext context)
        {
            _context = context;
            _table = _context.Set<TEntity>();
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _table.FindAsync(id);
        }

        public async Task<TEntity> GetByWithCriteriaAsync(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = _table;

            if (predicate is not null)
                query = query.Where(predicate);

            return await query.SingleOrDefaultAsync();
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = _table;

            if (predicate is not null)
                query = query.Where(predicate);

            return await query.ToListAsync();
        }

        public async Task InsertAsync(TEntity entity)
        {
            var addedRecord = await _table.AddAsync(entity);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await Task.Run(() => _table.Update(entity));
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await Task.Run(() => _table.Remove(entity));
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _table.AnyAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _table.CountAsync(predicate);
        }
    }
}
