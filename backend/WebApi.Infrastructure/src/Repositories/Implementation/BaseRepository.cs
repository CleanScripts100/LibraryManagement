
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WebApi.Domain.src.Abstractions;
using WebApi.Domain.src.Shared;
using WebApi.Infrastructure.src.Database;

namespace WebApi.Infrastructure.src.Repositories.Implementation
{
    public class BaseRepository<T> :IBaseRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;
        private readonly DatabaseContext _context;
        public BaseRepository(DatabaseContext dbContext)
        {
            _dbSet = dbContext.Set<T>();
            _context = dbContext;
        }

        public virtual async Task<T> CreateOne(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteOneById(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToArrayAsync();
        }

        public async Task<IEnumerable<T>> GetAll(QueryOptions queryOptions)
        {
            return await _dbSet.ToArrayAsync();
        }

        public async Task<T?> GetOneById(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> UpdateOneById(T updatedEntity)
        {
            _dbSet.Update(updatedEntity);
            await _context.SaveChangesAsync();
            return updatedEntity;
        }

        private IQueryable<T> ConstructQuery(Expression<Func<T, bool>> predicate, Func<IQueryable<T>,IOrderedQueryable<T>> orderBy, int? skip, int? take)
        {
            IQueryable<T> query = _dbSet;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip != null)
            {
                query = query.Skip(skip.Value);
            }

            if (take != null)
            {
                query = query.Take(take.Value);
            }
            return query;
        }
    }
}