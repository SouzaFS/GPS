using System.Linq.Expressions;
using GPS.Models;

namespace GPS.Repositories.Interfaces
{
    public interface IBaseRepository <T> where T : class {
        
        Task<T> CreateAsync(T entity); 
        IQueryable<T> GetAll();
        Task DeleteAsync(T entity);
        IQueryable<T> GetByWhere(Expression<Func<T, bool>> predicate);
        Task<T> UpdateAsync(T entity);

    }
}
