using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public async Task DeleteByIdAsync(int id)
        {
            var entityToDelete = await _dbSet.FindAsync(id);

            if (entityToDelete != null)
            {
                _dbSet.Remove(entityToDelete);
                await SaveAsync();
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return _dbSet.Find(id);
        }

        public async Task<List<T>> GetAllAsync(bool tracked = true, params string[] includes)
        {
            IQueryable<T> query = _dbSet;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            foreach (var inc in includes)
                query = query.Include(inc);

            return await query.ToListAsync();
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
