using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NLayerProjectForJwt.Core.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task AddAsync(TEntity entity);
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        void Remove(TEntity entity);
        TEntity Update(TEntity entity);

    }
}
