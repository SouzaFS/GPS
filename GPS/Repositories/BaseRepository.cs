using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GPS.DBContext;
using GPS.Repositories.Interfaces;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using ZstdSharp.Unsafe;

namespace GPS.Repositories{
    public class BaseRepository <T> : IBaseRepository <T> where T : class
    {
        private readonly IAppDBContext<T> _dbContext;
        private readonly IMongoCollection<T> collection;
        public BaseRepository(IAppDBContext<T> dbContext){
            _dbContext = dbContext;
            collection = _dbContext.GetCollection();
        }

        public async Task<T?> CreateAsync(T entity){
            
            await collection.InsertOneAsync(entity);
            return entity; 
        }

        public async Task<bool> DeleteAsync(T entity){
            
            var idProperty = entity.GetType().GetProperty("Id");
            if (idProperty != null){
                var idValue = idProperty.GetValue(entity);
                await collection.DeleteOneAsync(Builders<T>.Filter.Eq("Id", idValue));
                return true;
            }

            return false;
            
        }

        public async Task<List<T>?> GetAll(){
            
            var result = await collection.AsQueryable().ToListAsync();
            if (result != null){
                return result;
            }

            return null;
        }

        public async Task<T?> GetByWhere(Expression<Func<T, bool>> predicate){
            
            var result = await collection.AsQueryable().Where(predicate).FirstOrDefaultAsync();
            if (result != null){
                return result;
            }

            return null;
        }

        public async Task<T?> UpdateAsync(T entity){
            
            var idProperty = entity.GetType().GetProperty("Id");
            if(idProperty != null){
                var idValue = idProperty.GetValue(entity);
                await collection.ReplaceOneAsync(Builders<T>.Filter.Eq("Id", idValue), entity);
                return entity;
            }

            return null;
        }

    }

}