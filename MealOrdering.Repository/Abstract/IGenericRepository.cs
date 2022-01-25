using MealOrdering.Core.Entities.Abstract;
using System.Linq.Expressions;

namespace MealOrdering.Repository.Abstract
{
    public interface IGenericRepository<TEntity> where TEntity : class, IEntity, new()
    {
        Task<TEntity> GetByIdAsync(Guid id);

        Task<TEntity> GetAsync(params Expression<Func<TEntity, object>>[] includeProperties);

        Task<TEntity> GetByWithCriteriaAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> GetByWithCriteriaAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);

        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = default);

        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);

        Task InsertAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
