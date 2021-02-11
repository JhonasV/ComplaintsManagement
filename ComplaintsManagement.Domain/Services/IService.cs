using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintsManagement.Domain.Services
{
    public interface IService<TEntity> where TEntity: class
    {
        Task AddAsync(TEntity entity);
        Task DeleteAsync(int id);
        Task UpdateAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync(TEntity entity);
    }
}
