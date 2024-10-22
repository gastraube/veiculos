using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veiculos.Data.Repositories.Abstractions
{
    public interface IRepositoryBase<T> where T : class
    {
        Task AddAsync(T entity);
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync(bool tracked = true, params string[] includes);
        Task UpdateAsync(T entity);
        Task DeleteByIdAsync(int id);
        Task SaveAsync();
    }
}
