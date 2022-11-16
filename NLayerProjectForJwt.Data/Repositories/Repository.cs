using Microsoft.EntityFrameworkCore;
using NLayerProjectForJwt.Core.Repositories;
using NLayerProjectForJwt.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NLayerProjectForJwt.Data.Repositories
{
    public class Repository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }
        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            // bu arkadaş var ise memory de takip edilmesin dedik 
            if (entity != null) _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public TEntity Update(TEntity entity)
        {
            var updatedEntity = _context.Entry<TEntity>(entity);
            updatedEntity.State = EntityState.Modified;
            return updatedEntity.Entity;
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }
    }
}
