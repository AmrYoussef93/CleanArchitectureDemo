using CleanArchitectureDemo.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureDemo.Application.Common.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task AddRangeAsync(List<TEntity> entities);

        Task RemoveAsync(TEntity entity);
        Task RemoveRangeAsync(List<TEntity> entities);
        Task RemoveByIdAsync(int Id);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<TEntity> FindByIdAsync(int id);

        Task<TEntity> FindByExpressionAsync(Expression<Func<TEntity, bool>> expression);

        Task<IEnumerable<TEntity>> GetAllOrderedbyExpressionAsync(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, string>> order);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> where);

        Task<IList<TEntity>> GetAllIncludeingPropertiesAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties);
    }
}
