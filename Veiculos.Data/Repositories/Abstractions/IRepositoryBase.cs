using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Veiculos.Data.Repositories.Abstractions
{
    public interface IRepositoryBase<T> where T : class
    {
        Task AddAsync(T entity);
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, bool tracked = true, params string[] includes);
        Task UpdateAsync(T entity);
        Task DeleteByIdAsync(int id);
        Task SaveAsync();
        Task AddRangeAsync(List<T> entity);
        Task DeleteRange(List<T> entity);
    }
}
