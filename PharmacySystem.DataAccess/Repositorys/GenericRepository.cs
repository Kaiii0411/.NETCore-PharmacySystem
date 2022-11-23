using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PharmacySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.DataAccess.Repositorys
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IReadOnlyList<TEntity>> ListAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);

        Task<TEntity> AddAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<TEntity> DeleteAsync(TEntity entity);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null);
    }
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public readonly PharmacySystemContext _context;
        private readonly DbSet<TEntity> _dbSet;
        public GenericRepository(PharmacySystemContext context)
        {
            this._context = context;
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<TEntity>();
        }
        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            _context.Entry(entity);
            EntityEntry<TEntity> entityEntry = await _dbSet.AddAsync(entity, new CancellationToken());
            try
            {
                await _context.SaveChangesAsync(new CancellationToken());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return entityEntry.Entity;
        }
        public virtual async Task<TEntity> DeleteAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            int num = await _context.SaveChangesAsync();
            return num > 0 ? entity : null;
        }
        public virtual async Task<IReadOnlyList<TEntity>> ListAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> source = _dbSet.AsNoTracking();
            if (filter != null)
            {
                source = source.Where(filter);
            }

            if (orderBy != null)
            {
                return await orderBy(source).ToListAsync();
            }

            return await source.ToListAsync();
        }
        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> source = _dbSet.AsNoTracking();
            if (filter != null)
            {
                source = source.Where(filter);
            }

            return await source.CountAsync();
        }
        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            int num = await _context.SaveChangesAsync();
            return num > 0 ? entity : null;
        }
    }
}
