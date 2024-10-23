using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using Veiculos.Data.Repositories.Abstractions;
using Veiculos.Domain.Entity;

namespace Veiculos.Data.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly VeiculosDbContext _databaseContext;
        private readonly DbSet<T> _dbSet;

        public RepositoryBase(VeiculosDbContext context)
        {
            _databaseContext = context;
            _dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await SaveAsync();
        }

        public async Task AddRangeAsync(List<T> entity)
        {
            await _dbSet.AddRangeAsync(entity);
            await SaveAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entityToDelete = await _dbSet.FindAsync(id);

            if (entityToDelete != null)
            {
                _dbSet.Remove(entityToDelete);
                await SaveAsync();
            }
        }

        public async Task DeleteRange(List<T> entity)
        {
            if (entity != null)
            {
                _dbSet.RemoveRange(entity);
                await SaveAsync();
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return _dbSet.Find(id);
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, bool tracked = true, params string[] includes)
        {
            List<T> list;
            IQueryable<T> query = _dbSet;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            foreach (var inc in includes)
                query = query.Include(inc);

            if (predicate!=null)
                return await query.Where(predicate).ToListAsync<T>();

            return await query.ToListAsync<T>();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _databaseContext.SaveChangesAsync();
        }
    }
}
