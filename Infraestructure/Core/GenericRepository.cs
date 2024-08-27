
using Domain.Interface.Repositories;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Core
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly SocialRedContext _context;
        protected readonly DbSet<T> _dbSet;

        protected GenericRepository(SocialRedContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }


        public virtual async Task Add(T entity)
        {
            await _dbSet.AddAsync(entity);

        }

        public virtual async Task AddList(List<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);

        }

        public virtual async Task Delete(int id)
        {
            var entity = await GetById(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);

            }
        }

        public virtual async Task<List<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<T> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public virtual async Task Update(T entity)
        {

            _context.Entry(entity).State = EntityState.Modified;

        }
    }
}

