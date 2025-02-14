using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using GPS.DBContext;
using GPS.Models;
using GPS.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPS.Repositories{
    public class BaseRepository <T> : IBaseRepository <T> where T : class
    {
        private readonly AppDBContext _dbContext;
        private readonly DbSet<T> table = null;
        public BaseRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
            table = _dbContext.Set<T>();
        }

        public async Task<T> CreateAsync(T entity)
        {
            await table.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public IQueryable<T> GetAll()
        {
            return table.AsNoTracking();
        }

        public async Task DeleteAsync(T entity)
        {
            await table.Remove(entity).Context.SaveChangesAsync();
        }

        public IQueryable<T> GetByWhere(Expression<Func<T, bool>> predicate)
        {
            return table.Where(predicate).AsNoTracking();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            await table.Update(entity).Context.SaveChangesAsync();

            return entity;
        }
    }

}