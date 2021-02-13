using ComplaintsManagement.Domain.Entities;
using ComplaintsManagement.Domain.Repositories;
using ComplaintsManagement.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

namespace ComplaintsManagement.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbSet<TEntity> _entities;

        public Repository(DbSet<TEntity> entities)
        {

            _entities = entities;
        }

        public async Task AddAsync(TEntity entity) => await Task.Run(() => { _entities.Add(entity); });

        public void Delete(TEntity entity) => _entities.Remove(entity);
        public async Task DeleteAsync(TEntity entity) => await Task.Run(() => { _entities.Remove(entity);  });
        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate) => await _entities.AnyAsync(predicate);
        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _entities.ToListAsync();
        public async Task<TEntity> GetByIdAsync(int id) => await _entities.FindAsync(id);
        public async Task UpdateAsync(TEntity entity)
        {
             throw new NotImplementedException();
        }
    }
}