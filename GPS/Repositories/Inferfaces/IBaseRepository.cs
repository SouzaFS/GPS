using System.Linq.Expressions;
using GPS.Models;

namespace GPS.Repositories.Interfaces
{
    public interface IBaseRepository <T> where T : class {
        
        Task<T?> CreateAsync(T entity); 
        Task<List<T>?> GetAll();
        Task<bool> DeleteAsync(T entity);
        Task<T?> GetByWhere(Expression<Func<T, bool>> predicate);
        Task<T?> UpdateAsync(T entity);

    }
}
