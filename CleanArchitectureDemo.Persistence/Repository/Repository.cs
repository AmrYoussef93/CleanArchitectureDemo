using CleanArchitectureDemo.Application.Common.Interfaces;
using CleanArchitectureDemo.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureDemo.Persistence.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbSet = null;
        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public virtual async Task AddRangeAsync(List<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public virtual async Task<TEntity> FindByExpressionAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbSet.AsNoTracking().SingleOrDefaultAsync(expression);
        }

        public virtual async Task<TEntity> FindByIdAsync(int id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public virtual async Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> where)
        {
            return await _dbSet.AsNoTracking().Where(where).ToListAsync();
        }

        public virtual async Task<IList<TEntity>> GetAllIncludeingPropertiesAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var queryable = _dbSet.AsNoTracking().Where(where);

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    queryable = queryable.Include(includeProperty);
                }
            }
            return await queryable.ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllOrderedbyExpressionAsync(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, string>> order)
        {
            return await _dbSet.Where(where).OrderBy(order).ToListAsync();
        }

        public virtual async Task RemoveAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
        }
        public virtual async Task RemoveByIdAsync(int Id)
        {
            var row = await FindByIdAsync(Id);
            if (row != null)
            _dbSet.Remove(row);
        }

        public virtual async Task RemoveRangeAsync(List<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }
        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            _dbSet.Attach(entity);
            _dbSet.Update(entity);
            return entity;
        }
    }
}
